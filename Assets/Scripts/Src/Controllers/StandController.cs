using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Entities;
using Controllers;

public class StandController : MonoBehaviour
{
	public PlayerColour colour = PlayerColour.Gray;

	private PlayerController currentPlayer;

	private Image arrow;

	void Start ()
	{
		string arrowName = "BlueStandArrow";
		if (colour == PlayerColour.Red) arrowName = "RedStandArrow";
		else if (colour == PlayerColour.Yellow) arrowName = "YellowStandArrow";
		else if (colour == PlayerColour.Green) arrowName = "GreenStandArrow";

		arrow = GameObject.Find (arrowName).GetComponent<Image>();
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (currentPlayer == null && collision.gameObject.tag.Equals("Player"))
		{	
			currentPlayer = collision.gameObject.GetComponent<PlayerController> ();
			currentPlayer.player.Colour = colour;
			currentPlayer.ReloadSprites();
			arrow.canvasRenderer.SetAlpha(0.0f);
		}
	}
	
	void OnTriggerExit2D (Collider2D collision)
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			if (currentPlayer != null && currentPlayer.player.Id.Equals(collision.gameObject.GetComponent<PlayerController> ().player.Id))
			{
				if (currentPlayer.player.Colour == this.colour)
				{
					currentPlayer.player.Colour = PlayerColour.Gray;
					currentPlayer.ReloadSprites();
				}

				currentPlayer = null;
				arrow.canvasRenderer.SetAlpha(1.0f);
			}
		}
	}
}
