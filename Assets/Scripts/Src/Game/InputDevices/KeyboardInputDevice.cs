using UnityEngine;

namespace Game.InputDevices
{
	public class KeyboardPlayerMapping
	{
		public KeyboardPlayerMapping (KeyCode left, KeyCode right, KeyCode jump, KeyCode fire, KeyCode pause)
		{
			this.Left	= left;
			this.Right	= right;
			this.Jump	= jump;
			this.Fire	= fire;
			this.Pause	= pause;
		}

		public KeyCode Left		{ private set; get; }
		public KeyCode Right 	{ private set; get; }
		public KeyCode Jump 	{ private set; get; }
		public KeyCode Fire		{ private set; get; }
		public KeyCode Pause	{ private set; get; }
	}

	public class KeyboardInputDevice : InputDevice
	{
		private KeyboardPlayerMapping[] keyboardMappings = {
			new KeyboardPlayerMapping(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Space),
			new KeyboardPlayerMapping(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.Space),
			new KeyboardPlayerMapping(KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K, KeyCode.Space),
			new KeyboardPlayerMapping(KeyCode.C, KeyCode.B, KeyCode.F, KeyCode.V, KeyCode.Space)
		};

		override public float getAxisMovement()
		{
			if (UnityEngine.Input.GetKey(keyboardMappings[this.Id].Left))
			{
				return -1.0f;
			}
			else if (UnityEngine.Input.GetKey(keyboardMappings[this.Id].Right))
			{
				return 1.0f;
			}
			else
			{
				return 0.0f;
			}
		}

		override public bool isJumping()
		{
			return UnityEngine.Input.GetKeyDown(keyboardMappings[this.Id].Jump);
		}

		override public bool isShooting()
		{
			return UnityEngine.Input.GetKeyDown(keyboardMappings[this.Id].Fire);
		}

		override public bool isPausing()
		{
			return UnityEngine.Input.GetKeyDown(keyboardMappings[this.Id].Pause);
		}

		override public bool isStillConnected ()
		{
			return true;
		}
	}
}
