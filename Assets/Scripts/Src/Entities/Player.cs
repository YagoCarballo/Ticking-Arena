using Observers;

namespace Entities
{
	public class Player : Base, TimerObserver
	{
		private string spriteName;
		private float timeLeft;
		private float totalTime;

		public Player ()
		{
			this.setTag("Player");
		}

		public Player (int id) : this()
		{
			this.setId(id);
			this.setName("Player_" + id);
		}

		public string getSpriteName()
		{
			return spriteName;
		}

		public void setSpriteName(string spriteName)
		{
			this.spriteName = spriteName;
		}

		public float getTimeLeft()
		{
			return timeLeft;
		}

		public void setTimeLeft(float timeLeft)
		{
			this.timeLeft = timeLeft;
		}

		public float getTotalTime()
		{
			return totalTime;
		}

		public void setTotalTime(float totalTime)
		{
			this.totalTime = totalTime;
		}

		public void timerEnded(float time, int player)
		{

		}

		public void timerStarted(float time, int player)
		{

		}

		public void timerChangedOwner(int newPlayer, int oldPlayer)
		{

		}
	}
}
