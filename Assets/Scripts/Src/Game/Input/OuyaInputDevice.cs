namespace Game.Input
{
	public class OuyaInputDevice : InputDevice
	{
		private int ControllerID = 0;

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