namespace Observers
{
	public interface InputObserver
	{
		void inputMoved (bool left);
		void inputJumped ();
		void inputFired ();
	}
}