namespace Game.InputDevices
{
	public class OuyaInputDevice : InputDevice
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
	}
}