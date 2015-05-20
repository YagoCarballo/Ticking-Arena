using UnityEngine;
using System.Collections;
using Game;
using Entities;
using Controllers;
using Game.InputDevices;

public class EndOfBattleController : MonoBehaviour
{
	private GameManager game;
	public Arena arena;
	private GameObject playersObject;
	private GameObject standsObject;

	public Material RedStandMaterial;
	public Material GreenStandMaterial;
	public Material BlueStandMaterial;
	public Material YellowStandMaterial;

	void Awake ()
	{
		game = GameManager.Instance;
		playersObject = GameObject.Find ("Players");
		standsObject = GameObject.Find ("Stands");
		
		if (game.ActivePlayers != null && game.ActivePlayers[0] != null)
		{
			foreach (Player player in game.ActivePlayers) {
				GameObject prefab = (GameObject)Instantiate (Resources.Load ("Characters/Prefabs/Player"));
				prefab.name = player.Name;
				prefab.transform.localPosition = new Vector2(( player.Id * 3 ) - 4.5f, -2);

				prefab.GetComponent<PlayerController> ().player = player;
				prefab.GetComponent<PlayerController> ().endBattleMode = true;
				
				prefab.transform.parent = playersObject.transform;
				arena.addPlayer (player);
			}
		}
		else
		{
			// Debug Mode (Only when entering into the scene directly)
			for (int i=0;i<4;i++)
			{
				Player player	= new Player();
				player.Id		= i;
				player.Name		= "Player_" + i;
				player.InputInfo = new InputInfo();
				player.LastPosition = i + 1;
				
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
				prefab.transform.localPosition = new Vector2(( i * 3 ) - 4.5f, 3);

				prefab.GetComponent<PlayerController> ().player = player;

				prefab.transform.parent = playersObject.transform;
				arena.addPlayer(player);
			}
		}

	}

	void Start ()
	{
		game.UpdateCameraSize (Camera.main);

		int amountOfPlayers = arena.getAllPlayers ().Count;
		for (int i=0; i < 4; i++)
		{
			if (i < amountOfPlayers)
			{
				Player player = arena.getPlayer(i);
				standsObject.transform.GetChild (i).GetComponent<Renderer>().material = getStandMaterial(player.Colour);
				standsObject.transform.GetChild (i).GetComponent<Animator>().SetInteger("Position", player.LastPosition);
			}
			else
			{
				standsObject.transform.GetChild (i).gameObject.SetActive(false);
			}
		}
	}

	private Material getStandMaterial (PlayerColour color)
	{
		switch (color)
		{
		case PlayerColour.Red:
			return RedStandMaterial;
		case PlayerColour.Green:
			return GreenStandMaterial;
		case PlayerColour.Yellow:
			return YellowStandMaterial;
		default:
			return BlueStandMaterial;
		}
	}

	void NextScreen ()
	{
		Application.LoadLevel("CharacterSelector");
	}
}
