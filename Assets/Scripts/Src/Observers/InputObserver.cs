namespace Observers
{
	public interface InputObserver
	{
		void inputMoved (float axis);
		void inputJumped ();
		void inputFired ();
	}
}