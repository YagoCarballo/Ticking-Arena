using UnityEngine;
using System.Collections;

public class SidePortalController : MonoBehaviour
{
	public GameObject target;
	public bool spawnLeft = true;

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag.Equals("Player"))
		{
			if (spawnLeft)
			{
				collision.gameObject.transform.position = target.transform.position;
			}
			else
			{

			}
		}
	}
}
