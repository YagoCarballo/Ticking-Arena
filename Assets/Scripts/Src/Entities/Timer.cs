using Observers;
using System.Collections.Generic;

namespace Entities
{
	[System.Serializable]
	public class Timer : Base
	{
		// Observers
		private List<TimerObserver> timerObservers;

		private float startTime;
		private float endTime;
		private float speed;

		[UnityEngine.SerializeField]
		private int currentPlayer;
	}
}