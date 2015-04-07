namespace Game.Input
{
	public class KeyboardInputDevice : InputDevice
	{
		override public bool isMovingLeft()
		{
			return false;
		}

		override public bool isMovingRight()
		{
			return false;
		}

		override public bool isJumping()
		{
			return false;
		}

		override public bool isShooting()
		{
			return false;
		}
	}
}
