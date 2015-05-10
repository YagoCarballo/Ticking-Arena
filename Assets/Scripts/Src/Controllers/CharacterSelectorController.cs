using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Game;
using Controllers;

public class CharacterSelectorController : MonoBehaviour
{
	private GameManager game;
	public Arena arena;
	private GameObject playersObject;

	void Awake ()
	{
		game = GameManager.Instance;
		game.FindAvailableControllers ();

		playersObject = GameObject.Find ("Players");

		for (int i=0; i<4; i++) {
			Player player = new Player ();
			player.Id = i;
			player.Name = "Player_" + i;
			player.InputInfo = game.FindNextController ();
			
			switch (i) { 
			case 0:
				player.Colour = PlayerColour.Gray;
				player.Gender = PlayerGender.Female;
				break;
			case 1:
				player.Colour = PlayerColour.Gray;
				player.Gender = PlayerGender.Male;
				break;
			case 2:
				player.Colour = PlayerColour.Gray;
				player.Gender = PlayerGender.Male;
				break;
			case 3:
				player.Colour = PlayerColour.Gray;
				player.Gender = PlayerGender.Female;
				break;
			}

			GameObject prefab = (GameObject)Instantiate (Resources.Load ("Characters/Prefabs/Player"));
			prefab.name = player.Name;
			prefab.transform.localPosition = new Vector2 ((i * -1) + 1.5f, 2);
			
			prefab.GetComponent<PlayerController> ().player = player;
			prefab.GetComponent<PlayerController> ().selectorMode = true;
			
			prefab.transform.parent = playersObject.transform;
			arena.addPlayer (player);
		}
	}

	void Start ()
	{
		game.UpdateCameraSize (Camera.main);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StartGame();
		}
	}

	public void StartGame ()
	{
		List<Player> players = new List<Player> (4);

		foreach (Player player in arena.getAllPlayers())
		{
			if (player.Colour != PlayerColour.Gray)
			{
				players.Add(player);
			}
		}

		game.ActivePlayers = players.ToArray ();

		if (game.ActivePlayers.Length >= 2)
		{
			Application.LoadLevel ("Arena");
		}
	}
}
