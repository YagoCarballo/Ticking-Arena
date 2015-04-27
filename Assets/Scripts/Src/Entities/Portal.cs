namespace Entities
{
	[System.Serializable]
	public class Portal : Obstacle
	{
		[UnityEngine.SerializeField]
		private bool	_active			= true;

		[UnityEngine.SerializeField]
		private float	_waitingTime	= 2.0f;

		public Portal ()
		{
			this.Tag = "Portal";
		}

		public Portal (int id) : this()
		{
			this.Id		= id;
			this.Name	= "Portal_" + id;
		}

		public bool Active
		{
			set { this._active = value; }
			get { return this._active; }
		}

		public float WaitingTime
		{
			set { this._waitingTime = value; }
			get { return this._waitingTime; }
		}
	}
}
