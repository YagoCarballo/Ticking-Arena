using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entities;
using Game;
using Controllers;
using Game.InputDevices;

public class CharacterSelectorController : MonoBehaviour
{
	private GameManager game;
	public Arena arena;
	private GameObject playersObject;

	void Awake ()
	{
		game = GameManager.Instance;
		playersObject = GameObject.Find ("Players");
	}

	IEnumerator Start()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		while (!OuyaSDK.isIAPInitComplete())
		{
			yield return null;
		}
		#endif

		LoadPlayers ();
		ShowProperKeys ();
		game.UpdateCameraSize (Camera.main);
		yield return true;
	}

	private void ShowProperKeys ()
	{
		int ouyaControllers = OuyaInputDevice.GetConnectedControllers ().Length;
		Debug.Log (ouyaControllers);
		if (ouyaControllers != 0)
		{
			foreach (GameObject button in GameObject.FindGameObjectsWithTag("keyboard-button"))
			{
				button.SetActive(false);
			}
		}
		else
		{
			foreach (GameObject button in GameObject.FindGameObjectsWithTag("ouya-button"))
			{
				button.SetActive(false);
			}
		}
	}

	private void LoadPlayers ()
	{
		game.FindAvailableControllers ();
		
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
			Application.LoadLevel ("ArenaSelector");
		}
	}
}
