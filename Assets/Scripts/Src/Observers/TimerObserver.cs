namespace Observers
{
	public interface TimerObserver
	{
		void timerEnded (float time, int player);
		void timerStarted (float time, int player);
		void timerChangedOwner (int newPlayer, int oldPlayer);
	}
}