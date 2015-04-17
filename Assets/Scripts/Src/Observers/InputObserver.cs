namespace Observers
{
	public interface InputObserver
	{
		void InputDetected (float axis, bool jump, bool fire);
	}
}