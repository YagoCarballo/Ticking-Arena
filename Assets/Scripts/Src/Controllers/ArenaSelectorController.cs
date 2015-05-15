using UnityEngine;
using System.Collections;

public class ArenaSelectorController : MonoBehaviour
{
	public void LoadPortalsArena ()
	{
		Application.LoadLevel ("PortalsArena");
	}

	public void LoadBouncyCastleArena ()
	{
		Application.LoadLevel ("BouncyCastleArena");
	}

	public void LoadNeverEndingArena ()
	{
		Application.LoadLevel ("PortalsArena");
	}
}
