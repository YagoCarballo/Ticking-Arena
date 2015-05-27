using UnityEngine;
using System.Collections;
using Controllers;

public class GravityChangerController : MonoBehaviour
{
	public float gravityLevel = -9.8f;

	void Update () {
		Physics2D.gravity = new Vector2(0.0f, gravityLevel);
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.tag.Equals("Player"))
		{
			this.gravityLevel = -gravityLevel;
		}
	}

	void OnDestroy ()
	{
		Physics2D.gravity = new Vector2(0.0f, -9.8f);
	}
}
