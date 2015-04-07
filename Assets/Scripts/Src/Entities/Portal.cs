namespace Entities
{
	public class Portal : Obstacle
	{
		public Portal ()
		{
			this.setTag("Player");
		}

		public Portal (int id) : this()
		{
			this.setId(id);
			this.setName("Portal_" + id);
		}
	}
}
