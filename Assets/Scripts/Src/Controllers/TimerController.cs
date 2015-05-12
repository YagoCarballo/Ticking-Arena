using Entities;
using UnityEngine;
using Observers;
using Controllers;

namespace controllers
{
	public class TimerController : MonoBehaviour, ActivePlayerObserver, TimerObserver
	{
		[UnityEngine.SerializeField]
		public Timer timer;

		// Follower
		public GameObject player;
		private Transform playersObject;
		private Renderer timerRenderer;

		public bool ready = true;
		public bool goingBack = false;
		public float MovingSpeed = 300.0f;
		
		private bool moving;
		private Vector3 destination;
		private Quaternion	lookAtRotation;

		// UI
		private ProgressBarController progressBar;
		private Animator endMessageAnimator;

		public void Awake ()
		{
			progressBar = GameObject.FindWithTag ("progress-bar").GetComponent<ProgressBarController> ();
			endMessageAnimator = GameObject.Find ("BattleEndedMessage").GetComponent<Animator> ();

			timerRenderer = GetComponent<Renderer> ();
			playersObject = GameObject.Find ("Players").transform;
			timer = new Timer (this);
		}

		public void Start ()
		{
			timer.CurrentPlayer = Random.Range (0, playersObject.childCount);
			player = playersObject.GetChild(timer.CurrentPlayer).gameObject;

			// Updates GUI
			progressBar.progress = 1.0f;
			progressBar.timeLeft = (int) timer.TimeToWait;
			progressBar.color = player.GetComponent<PlayerController> ().player.Colour;

			timer.StartTime = Time.time;
		}

		public void Update ()
		{
			if (timer.Ended && Input.GetKeyDown(KeyCode.Space))
			{
				Application.LoadLevel("CharacterSelector");
			}

			// Handles the timer
			timer.CurrentTime = (Time.time - timer.StartTime);
			float progress = timer.CheckStatus ();

			// Updates GUI
			progressBar.progress = 1.0f - progress;
			progressBar.timeLeft = (int) (timer.TimeToWait - timer.CurrentTime);

			// timer color
			if (progress >= 0.7f) timerRenderer.material.color = new Color(0.89f, 0.0f, 0.10f);
			else if (progress >= 0.4f) timerRenderer.material.color = new Color(1.0f, 0.8f, 0);
			else timerRenderer.material.color = new Color(0.45f, 0.73f, 0.19f);

			// Follower
			if (!ready)
			{
				if (goingBack)
				{
					destination = player.transform.position;
				}
				
				// Get the Distance from the follower to the spot
				Vector3 distance = (destination - transform.position);
				
				// Movement Speed for the Rotation
				float speed = (MovingSpeed * Time.deltaTime);
				moving = false;
				
				// If the Follower is too far away speed up
				if (distance.x >  0.1f || distance.y >  0.1f || distance.z >  0.1f || 
				    distance.x < -0.1f || distance.y < -0.1f || distance.z <  -0.1f)
				{
					speed = (MovingSpeed * Time.deltaTime);
					moving = true;
				}
				
				if (moving)
				{
					// rotate to look at the destiny
					lookAtRotation = Quaternion.LookRotation(distance);
					
					// Animate Rotation
					transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, speed);
					
					//move towards the destiny
					transform.position += transform.forward * speed * Time.deltaTime;
				}
				else
				{
					if (!goingBack)
					{
						goingBack = true;
					}
					else
					{
						ready = true;
						goingBack = false;
					}
				}
				
				transform.position = new Vector3(
					transform.position.x, 
					transform.position.y, 
					player.transform.position.z
					);
			}
			else
			{
				transform.position = new Vector3(
					player.transform.position.x,
					player.transform.position.y + 0.55f,
					player.transform.position.z - 1.0f
					);
			}
		}

		public void OnEnable ()
		{
			timer.addTimeObserver (this);
		}

		public void OnDisable ()
		{
			this.timer.TimerObservers.RemoveRange (0, this.timer.TimerObservers.Count);
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.gameObject.tag.Equals("Player"))
			{
				PlayerController playerController = collider.gameObject.GetComponent<PlayerController> ();
				if (playerController.player.Id != timer.CurrentPlayer)
				{
					ready = true;
					goingBack = false;
					player = playersObject.GetChild(playerController.player.Id).gameObject;

					// Updates GUI
					progressBar.progress = 1.0f;
					progressBar.timeLeft = (int) timer.TimeToWait;
					progressBar.color = player.GetComponent<PlayerController> ().player.Colour;

					timer.CurrentPlayer = playerController.player.Id;
					timer.StartTime = Time.time;
				}
			}
		}

		public void ThrowTimer (bool facingRight)
		{
			if (facingRight)
			{
				destination = player.transform.position + Vector3.right * 5.0f;
			}
			else
			{
				destination = player.transform.position + Vector3.left * 5.0f;
			}
			
			ready = false;
		}

		#region TimerObserver implementation

		public void timerEnded (float time, int player)
		{
			endMessageAnimator.SetTrigger ("End");
		}

		public void timerStarted (float time, int player)
		{
		}

		public void timerChangedOwner (int newPlayer, int oldPlayer, ActivePlayerObserver observer)
		{
		}

		#endregion
	}
}