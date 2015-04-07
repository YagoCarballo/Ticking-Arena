using Observers;
using System.Collections.Generic;

namespace Entities
{
	public class Timer : Base
	{
		// Observers
		private List<TimerObserver> timerObservers;

		private float startTime;
		private float endTime;
		private float speed;

		private int currentPlayer;
	}
}