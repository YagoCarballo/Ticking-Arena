using Entities;
using Game.InputDevices;
using Observers;
using UnityEngine;
using Game;

namespace Controllers
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(PolygonCollider2D))]

	public class PlayerController : MonoBehaviour, InputObserver
	{
		public	Player			player;
		private GameManager		game;
		private InputDevice		inputHandler;
		private SpriteParser	spriteParser;
		private Rigidbody2D		rigidBody;

		// Movement Variables
		public	float	maxSpeedX	= 5f;
		private	float	inputAxis	= 0f;
		private float	velocityX;
		private	bool	facingRight	= true;

		// Jump Variables
		public	float	maxSpeedY	= 10f;

		public void Awake ()
		{
			game = GameManager.Instance;
			this.spriteParser = new SpriteParser ("Characters/Sprites/Player-Sprite");
			this.rigidBody = GetComponent<Rigidbody2D> ();
		}

		public void Start () 
		{
			if (player.InputInfo == null)
			{
				gameObject.SetActive(false);
			}
			else
			{
				if (player.InputInfo.Type == typeof(KeyboardInputDevice))
				{
					this.inputHandler = gameObject.AddComponent<KeyboardInputDevice> ();
				}
				else if (player.InputInfo.Type == typeof(OuyaInputDevice))
				{
					this.inputHandler = gameObject.AddComponent<OuyaInputDevice> ();
				}
				
				this.inputHandler.Id = player.InputInfo.Id;
				this.inputHandler.AddObserver (this);
			}
			
			ReloadSprites ();
		}


		public void Update ()
		{
		}

		public void OnEnable ()
		{
			if (inputHandler != null)
			{
				inputHandler.AddObserver (this);
			}
		}

		public void OnDisable ()
		{
			if (inputHandler != null)
			{
				inputHandler.RemoveObserver (this);
			}
		}

		public void OnCollisionEnter2D () {}
		public void OnCollisionExit2D () {}
		public void OnCollisionStay2D () {}


		public void inputMoved(float axis)
		{
			inputAxis = axis;
			this.rigidBody.velocity = new Vector2 ((axis * this.maxSpeedX), this.rigidBody.velocity.y);

			if (axis < 0 && facingRight)
			{
				Flip();
			}
			else if (axis > 0 && !facingRight)
			{
				Flip();
			}
		}

		public void inputJumped()
		{
			this.rigidBody.velocity = new Vector2 (this.rigidBody.velocity.x, this.maxSpeedY);
		}

		public void inputFired()
		{
			Debug.Log (player.Name + " Fired");
		}

		private void Flip()
		{
			// Flip the Character
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

		public void ReloadSprites ()
		{
			string spriteName = player.getSpriteName ();
			UpdateSprite ("Head", spriteName);
			UpdateSprite ("Body", spriteName);
			UpdateSprite ("Arm", spriteName);
			UpdateSprite ("Arm-Behind", spriteName);
			UpdateSprite ("Leg", spriteName);
			UpdateSprite ("Leg-Behind", spriteName);
		}

		private void UpdateSprite (string bodyPartName, string spriteName)
		{
			Transform bodyPart = gameObject.transform.FindChild (bodyPartName);
			bodyPart.GetComponent<SpriteRenderer> ().sprite = spriteParser.GetSprite(spriteName + "-" + bodyPartName);

			if (this.player.Gender == PlayerGender.Male)
			{
				this.GetComponent<PolygonCollider2D>().points = Player.MaleCollisionPoints;

				if (bodyPartName.Equals("Leg"))
				{
					bodyPart.GetComponent<Renderer>().sortingOrder = 3;
				}
			}
			else
			{
				this.GetComponent<PolygonCollider2D>().points = Player.FemaleCollisionPoints;

				if (bodyPartName.Equals("Leg"))
				{
					bodyPart.GetComponent<Renderer>().sortingOrder = 1;
				}
			}
		}
	}
}
