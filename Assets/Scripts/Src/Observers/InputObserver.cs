namespace Observers
{
	public interface InputObserver
	{
		void InputDetected (float axis, bool jump, bool fire, bool pausing);
		void InputConnectionUpdated (bool connected);
	}
}