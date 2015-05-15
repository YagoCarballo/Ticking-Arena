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
		private float timeToWait		= 30f;
		private int	  numberOfChanges	= 0;
		private float maxChanges		= 6f;
		private float maxTime			= 30f;

		[UnityEngine.SerializeField]
		private float speed = 5f;

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
				if (this.CurrentTime >= this.TimeToWait)
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
				this.TimeToWait		= this.maxTime * ((this.maxChanges - this.numberOfChanges) / this.maxChanges);

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
					this.numberOfChanges++;
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
			set {
				if (value > 4.0f)
				{
					this.timeToWait = value;
				}
				else
				{
					this.timeToWait = 4.0f; 
				}
			}
		}

		public float Speed
		{
			get { return this.speed; }
			set { this.speed = value; }
		}

		public int NumberOfChanges
		{
			get { return this.numberOfChanges; }
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