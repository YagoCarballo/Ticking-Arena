using Observers;
using System.Collections.Generic;

namespace Entities
{
	[System.Serializable]
	public class Timer : Base
	{
		// Observers
		private List<TimerObserver> timerObservers;
		private ActivePlayerObserver activePlayerObserver;

		private float startTime;
		private float currentTime;
		private float progress;
		private bool  ended;

		[UnityEngine.SerializeField]
		private float speed = 5f;

		[UnityEngine.SerializeField]
		private float timeToWait = 20f;

		[UnityEngine.SerializeField]
		private int currentPlayer = -1;

		public Timer (ActivePlayerObserver activePlayerObserver)
		{
			this.Name	= "Timer";
			this.Tag	= "Timer";
			this.timerObservers = new List<TimerObserver> (4);
			this.activePlayerObserver = activePlayerObserver;
		}

		public float CheckStatus ()
		{
			if (!this.ended)
			{
				if ((this.CurrentTime - this.StartTime) >= this.TimeToWait)
				{
					this.ended = true;

					foreach (TimerObserver observer in this.timerObservers)
					{
						observer.timerEnded(this.currentTime, this.currentPlayer);
					}
				}
			}

			return this.progress;
		}

		public float StartTime
		{
			get { return this.startTime; }
			set {
				this.startTime		= value;
				this.currentTime	= 0.0f;
				this.progress		= 0.0f;
				this.ended			= false;

				foreach (TimerObserver observer in this.timerObservers)
				{
					observer.timerStarted(this.currentTime, this.currentPlayer);
				}
			}
		}

		public float CurrentTime
		{
			get { return this.currentTime; }
			set {
				this.currentTime = value;
				this.progress = this.currentTime / this.timeToWait;
			}
		}

		public int CurrentPlayer
		{
			get { return this.currentPlayer; }
			set {
				int oldPlayer = this.currentPlayer;
				this.currentPlayer = value;

				if (oldPlayer != this.currentPlayer)
				{
					foreach (TimerObserver observer in this.timerObservers)
					{
						observer.timerChangedOwner(this.currentPlayer, oldPlayer, activePlayerObserver);
					}
				}
			}
		}

		public float Progress
		{
			get { return this.progress; }
		}

		public bool Ended
		{
			get { return this.ended; }
		}

		public float TimeToWait
		{
			get { return this.timeToWait; }
			set { this.timeToWait = value; }
		}

		public float Speed
		{
			get { return this.speed; }
			set { this.speed = value; }
		}

		public List<TimerObserver> TimerObservers
		{
			get { return this.timerObservers; }
			private set { this.timerObservers = value; }
		}

		public void addTimeObserver (TimerObserver observer)
		{
			this.timerObservers.Add (observer);
		}

		public void removeTimeObserver (TimerObserver observer)
		{
			this.timerObservers.Remove (observer);
		}
	}
}