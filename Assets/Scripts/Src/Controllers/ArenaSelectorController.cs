using UnityEngine;
using System.Collections;
using Game;
using Game.InputDevices;

public class ArenaSelectorController : MonoBehaviour
{
	void Start ()
	{
		ShowProperKeys ();
		GameManager.Instance.UpdateCameraSize (Camera.main);
	}

	public void LoadPortalsArena ()
	{
		GameManager.Instance.LastPlayedArena = "PortalsArena";
		Application.LoadLevel (GameManager.Instance.LastPlayedArena);
	}

	public void LoadBouncyCastleArena ()
	{
		GameManager.Instance.LastPlayedArena = "BouncyCastleArena";
		Application.LoadLevel (GameManager.Instance.LastPlayedArena);
	}

	public void LoadNeverEndingArena ()
	{
		GameManager.Instance.LastPlayedArena = "NeverEndingArena";
		Application.LoadLevel (GameManager.Instance.LastPlayedArena);
	}

	public void LoadGravityArena ()
	{
		GameManager.Instance.LastPlayedArena = "GravityArena";
		Application.LoadLevel (GameManager.Instance.LastPlayedArena);
	}

	private void ShowProperKeys ()
	{
		int ouyaControllers = OuyaInputDevice.GetConnectedControllers ().Length;

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
}
