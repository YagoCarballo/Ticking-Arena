using UnityEngine;
using System.Collections;
using Game;

public class ArenaSelectorController : MonoBehaviour
{
	void Start ()
	{
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
}
