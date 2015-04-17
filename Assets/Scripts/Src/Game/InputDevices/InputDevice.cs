using Observers;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Game.InputDevices
{	
	public class InputInfo
	{
		public Type	Type		{ set; get; }
		public int	Id			{ set; get; }
		public bool	Available	{ set; get; }

		public InputInfo (int id, Type type, bool available)
		{
			this.Id = id;
			this.Type = type;
			this.Available = available;
		}

		public InputInfo () : this(-1, typeof(InputDevice), true)
		{
		}
	}

	public abstract class InputDevice : MonoBehaviour
	{
		private List<InputObserver> observers = new List<InputObserver> (1);
		public int Id { set; get; }

		public abstract float getAxisMovement ();
		public abstract bool isJumping ();
		public abstract bool isShooting ();

		public void Update ()
		{
			float axis		= this.getAxisMovement ();
			bool jumping	= this.isJumping ();
			bool shooting	= this.isShooting ();

			foreach (InputObserver observer in observers)
			{
				observer.InputDetected (axis, jumping, shooting);
			}
		}

		public void AddObserver (InputObserver observer)
		{
			this.observers.Add (observer);
		}

		public void RemoveObserver (InputObserver observer)
		{
			this.observers.Remove (observer);
		}
}
}
