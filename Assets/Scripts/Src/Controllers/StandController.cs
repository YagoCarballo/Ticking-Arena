using UnityEngine;
using System.Collections;
using Entities;
using Controllers;

public class StandController : MonoBehaviour
{
	public PlayerColour colour = PlayerColour.Gray;

	private PlayerController currentPlayer;

	void OnCollisionStay2D (Collision2D collision)
	{
		if (currentPlayer == null && collision.gameObject.tag.Equals("Player"))
		{	
			currentPlayer = collision.gameObject.GetComponent<PlayerController> ();
			currentPlayer.player.Colour = colour;
			currentPlayer.ReloadSprites();
		}
	}
	
	void OnCollisionExit2D (Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			if (currentPlayer.player.Id.Equals(collision.gameObject.GetComponent<PlayerController> ().player.Id))
			{
				currentPlayer.player.Colour = PlayerColour.Gray;
				currentPlayer.ReloadSprites();
				currentPlayer = null;
			}
		}
	}
}
