using UnityEngine;
using System.Collections;
using Game;

public class ArenaSelectorController : MonoBehaviour
{
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
}
