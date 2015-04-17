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
		public	float	MaxSpeedX	= 10f;
		private	float	inputAxis	= 0f;
		private float	velocityX;
		private	bool	facingRight	= true;
		private bool	disableWalk = false;

		// Jump Variables
		public	int		MaxJumps	= 2;
		public	float	MaxSpeedY	= 10f;
		private float	velocityY;
		private	int		jumpCount;

		// World Variables
		[Range(-100.0f, 100.0f)]
		public float gravityLevel = 0.0f;

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
			// Sets the Gravity level on the Y axis, (so the Jumps are more realistic and fast)
			Physics.gravity = Vector3.up * gravityLevel;
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

		void OnCollisionEnter2D(Collision2D collision) {
			// Reset the count of jumps if the player hit an obstacle
			if (collision.gameObject.tag.Equals("obstacle"))
			{
				jumpCount = 0;
				
				Vector3 contactPoint = collision.contacts[0].point;
				Vector3 center = GetComponent<Collider2D>().bounds.center;
				
				bool right = contactPoint.x > (center.x + MaxSpeedX);
				bool left = contactPoint.x < (center.x - MaxSpeedX);
				bool top = contactPoint.y > (center.y - MaxSpeedY);
				bool bottom = contactPoint.y > (center.y - MaxSpeedY);

//				if (!right && !left && !top)
//				{
//					// Sides
//					disableWalk = false;
//				}
//				else
//				{
//					// Top / Bottom
//					disableWalk = true;
//				}

			}
		}

		#region InputObserver implementation

		public void InputDetected (float axis, bool jump, bool fire)
		{
			float forceX = 0;
			float forceY = 0;
			velocityX = GetComponent<Rigidbody2D>().velocity.x;
			velocityY = GetComponent<Rigidbody2D>().velocity.y;

			// If the Player did not exceed the Jumping limit
			if (jumpCount < MaxJumps)
			{
				// Sets the new Horizontal force 0
				if(axis != 0 & !disableWalk)
				{
					forceX = MaxSpeedX * axis;
				}
				
				// If the Player just jumped (not moved) set the Vertical force
				if(jump)
				{
					forceY = MaxSpeedY;
					jumpCount++;
				}
				// if the user did not jump, leave the gravity do it's job
				else
				{
					forceY = GetComponent<Rigidbody2D>().velocity.y;
				}
			} 
			else
			{
				// Reset the position if the player released the jump button
				if(!jump)
				{
					forceY = GetComponent<Rigidbody2D>().velocity.y;
					forceX = GetComponent<Rigidbody2D>().velocity.x;
				}
			}
			
			// applies the new forces to the player
			GetComponent<Rigidbody2D>().velocity = new Vector2 (forceX, forceY);
			
			// Flip the character if it's looking at the wrong side
			if (jumpCount < MaxJumps)
			{
				if (axis > 0 && !facingRight)
				{
					Flip ();
				}
				else if (axis < 0 && facingRight)
				{
					Flip ();
				}
			}
		}

		#endregion

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
