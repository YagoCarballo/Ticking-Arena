using UnityEngine;
using System.Collections;
using Entities;
using Game;
using Controllers;
using controllers;

public class ArenaController : MonoBehaviour
{
	private GameManager game;
	public Arena arena;
	private GameObject playersObject;

	void Awake ()
	{
		game = GameManager.Instance;
		game.FindAvailableControllers ();

		GameObject timerPrefab = (GameObject) Instantiate(Resources.Load("Items/Timer/Prefabs/Timer"));

		playersObject = GameObject.Find ("Players");

		if (game.ActivePlayers.Length >= 2)
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
			for (int i=0;i<4;i++)
			{
				// TODO: Get the Players list from the GameManager
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
			timerPrefab.GetComponent<TimerController> ().timer.addTimeObserver(playersObject.transform.GetChild(i).GetComponent<PlayerController> ());
		}

		timerPrefab.transform.parent = gameObject.transform;
	}

	void Start ()
	{
		game.UpdateCameraSize (Camera.main);
	}

	void Update ()
	{

	}
}
