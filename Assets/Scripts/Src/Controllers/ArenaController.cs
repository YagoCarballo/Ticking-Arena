using UnityEngine;
using System.Collections;
using Entities;
using Game;
using Controllers;
using controllers;
using Observers;

public class ArenaController : MonoBehaviour, TimerObserver
{
	private GameManager game;
	public Arena arena;
	private GameObject playersObject;
	private TimerController timer;
	private int positions = 4;
	private Animator endMessageAnimator;

	void Awake ()
	{
		game = GameManager.Instance;

		GameObject timerPrefab = (GameObject) Instantiate(Resources.Load("Items/Timer/Prefabs/Timer"));
		timer = timerPrefab.GetComponent<TimerController> ();

		playersObject = GameObject.Find ("Players");
		endMessageAnimator = GameObject.Find ("BattleEndedMessage").GetComponent<Animator> ();

		if (game.ActivePlayers != null && game.ActivePlayers[0] != null)
		{
			foreach (Player player in game.ActivePlayers) {
				GameObject prefab = (GameObject)Instantiate (Resources.Load ("Characters/Prefabs/Player"));
				prefab.name = player.Name;
				prefab.transform.localPosition = new Vector2 ((player.Id * -1) + 1.5f, -2);
			
				prefab.GetComponent<PlayerController> ().player = player;
			
				prefab.transform.parent = playersObject.transform;
				arena.addPlayer (player);
			}
		}
		else
		{
			game.FindAvailableControllers ();

			// Debug Mode (Only when entering into the scene directly)
			for (int i=0;i<4;i++)
			{
				Player player	= new Player();
				player.Id		= i;
				player.Name		= "Player_" + i;
				player.InputInfo = game.FindNextController ();

				switch (i) { 
					case 0:
						player.Colour = PlayerColour.Blue;
						player.Gender = PlayerGender.Female;
						break;
					case 1:
						player.Colour = PlayerColour.Red;
						player.Gender = PlayerGender.Male;
						break;
					case 2:
						player.Colour = PlayerColour.Yellow;
						player.Gender = PlayerGender.Male;
						break;
					case 3:
						player.Colour = PlayerColour.Green;
						player.Gender = PlayerGender.Female;
						break;
				}
			
				GameObject prefab = (GameObject) Instantiate(Resources.Load("Characters/Prefabs/Player"));
				prefab.name = player.Name;
				prefab.transform.localPosition = new Vector2(( i * -1 ) + 1.5f, -2);

				prefab.GetComponent<PlayerController> ().player = player;

				prefab.transform.parent = playersObject.transform;
				arena.addPlayer(player);
			}
		}

		for (int i=0;i<playersObject.transform.childCount;i++)
		{
			timer.timer.addTimeObserver(playersObject.transform.GetChild(i).GetComponent<PlayerController> ());
		}

		timer.timer.addTimeObserver (this);
		timerPrefab.transform.parent = gameObject.transform;
		positions = arena.getAllPlayers ().Count;
	}

	void Start ()
	{
		game.UpdateCameraSize (Camera.main);
	}

	void Update ()
	{

	}

	#region TimerObserver implementation

	public void timerEnded (float time, int playerId)
	{
		arena.getPlayer (playerId).LastPosition = positions;
		game.ActivePlayers [playerId] = arena.getPlayer (playerId);
		playersObject.transform.GetChild (playerId).gameObject.SetActive (false);
		playersObject.transform.GetChild (playerId).GetComponent<PlayerController> ().FreezePlayer = true;
		positions--;

		if (positions == 1)
		{
			endMessageAnimator.SetTrigger ("End");

			foreach (Player player in game.ActivePlayers)
			{
				if (player.LastPosition == 0)
				{
					player.LastPosition = positions;
					game.ActivePlayers [player.Id] = player;
					Application.LoadLevel("EndOfBattleMenu");
					return;
				}
			}
		}
		else
		{
			timer.Start();
		}
	}

	public void timerStarted (float time, int player) {}
	public void timerChangedOwner (int newPlayer, int oldPlayer, ActivePlayerObserver observer) {}

	#endregion
}
