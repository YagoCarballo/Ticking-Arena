namespace Entities
{
	[System.Serializable]
	public class Portal : Obstacle
	{
		public Portal ()
		{
			this.Tag = "Player";
		}

		public Portal (int id) : this()
		{
			this.Id		= id;
			this.Name	= "Portal_" + id;
		}
	}
}
