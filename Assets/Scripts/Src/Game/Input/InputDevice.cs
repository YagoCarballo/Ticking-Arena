using Observers;
using System.Collections.Generic;

namespace Game.Input
{	
	public abstract class InputDevice
	{
		private List<InputObserver> observers;

		public abstract bool isMovingLeft ();
		public abstract bool isMovingRight ();
		public abstract bool isJumping ();
		public abstract bool isShooting ();

		public void Update ()
		{
			if (this.isJumping()) {

			}

			if (this.isMovingLeft()) {

			}

			if (this.isMovingRight()) {

			}

			if (this.isShooting()) {

			}
		}

		public void addObserver (InputObserver observer)
		{
		}

		public void removeObserver (InputObserver observer)
		{
		}
}
}
