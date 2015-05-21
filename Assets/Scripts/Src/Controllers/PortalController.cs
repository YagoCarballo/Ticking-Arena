using Entities;
using UnityEngine;
using System.Collections;

namespace Controllers
{
	[RequireComponent(typeof(AudioSource))]
	public class PortalController : MonoBehaviour
	{
		private AudioSource audioSource;
		public AudioClip portalCrossed;

		public Portal Portal;
		public Transform NextPortal;

		public void Awake ()
		{
			audioSource = GetComponent<AudioSource> ();
		}

		public void Start () {}

		public void OnEnable () {}
		public void OnDisable () {}

		public void Update ()
		{
			if (!Portal.Active)
			{
				StartCoroutine("ReEnablePortal");
			}
		}

		public void OnTriggerEnter2D (Collider2D collider)
		{
			if (Portal.Active && !collider.gameObject.tag.Equals("obstacle"))
			{
				audioSource.PlayOneShot(portalCrossed);

				Portal.Active = false;
				NextPortal.GetComponent<PortalController>().Portal.Active = false;
				collider.gameObject.transform.position = NextPortal.position;
			}
		}

		IEnumerator ReEnablePortal () {
			yield return new WaitForSeconds(Portal.WaitingTime);
			Portal.Active = true;
		}
	}
}