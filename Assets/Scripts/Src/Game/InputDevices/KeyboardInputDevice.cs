using UnityEngine;

namespace Game.InputDevices
{
	public class KeyboardPlayerMapping
	{
		public KeyboardPlayerMapping (KeyCode left, KeyCode right, KeyCode jump, KeyCode fire)
		{
			this.Left	= left;
			this.Right	= right;
			this.Jump	= jump;
			this.Fire	= fire;
		}

		public KeyCode Left		{ private set; get; }
		public KeyCode Right 	{ private set; get; }
		public KeyCode Jump 	{ private set; get; }
		public KeyCode Fire		{ private set; get; }
	}

	public class KeyboardInputDevice : InputDevice
	{
		private KeyboardPlayerMapping[] keyboardMappings = {
			new KeyboardPlayerMapping(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S),
			new KeyboardPlayerMapping(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow),
			new KeyboardPlayerMapping(KeyCode.J, KeyCode.L, KeyCode.I, KeyCode.K),
			new KeyboardPlayerMapping(KeyCode.C, KeyCode.B, KeyCode.F, KeyCode.V)
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
	}
}
