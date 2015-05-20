namespace Game.InputDevices
{
	public class InputDisabledDevice : InputDevice
	{
		override public float getAxisMovement()
		{
			return 0.0f;
		}
		
		override public bool isJumping()
		{
			return false;
		}
		
		override public bool isShooting()
		{
			return false;
		}

		override public bool isPausing()
		{
			return false;
		}

		override public bool isStillConnected ()
		{
			return true;
		}
	}
}