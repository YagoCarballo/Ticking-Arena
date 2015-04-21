using Entities;
using UnityEngine;
using System.Collections;

namespace Controllers
{	
	[RequireComponent(typeof(Animator))]
	public class PortalButtonController : MonoBehaviour
	{
		private Animator animator;

		private GameObject[] portals;
		private int[] openedPortals = { -1, -1 };

		public void Awake ()
		{
			animator = GetComponent<Animator> ();
		}

		public void Start ()
		{
			portals = GameObject.FindGameObjectsWithTag ("portal");
			ReloadPortals ();
		}

		public void OnTriggerEnter2D (Collider2D collider) 
		{
			if (collider.gameObject.tag.Equals("Player"))
			{
				ReloadPortals ();
				animator.SetTrigger("Pressed");
			}
		}

		private void ReloadPortals ()
		{
			foreach (GameObject portal in portals)
			{
				portal.SetActive(false);
			}
			
			openedPortals[0] = Random.Range(0, portals.Length - 1);
			do {
				openedPortals[1] = Random.Range(0, portals.Length - 1);
			} while (openedPortals[0] == openedPortals[1]);
			
			portals [openedPortals[0]].GetComponent<PortalController>().NextPortal = portals [openedPortals[1]].transform;
			portals [openedPortals[1]].GetComponent<PortalController>().NextPortal = portals [openedPortals[0]].transform;
			
			portals[openedPortals[0]].SetActive(true);
			portals[openedPortals[1]].SetActive(true);
		}
	}
}