namespace Entities
{
	public class Obstacle : Base
	{
		public readonly static int UNKNOWN    = -1;
		public readonly static int WALL       = 0;
		public readonly static int FLOOR      = 1;
		public readonly static int CEILING    = 2;
		public readonly static int PLATFORM   = 3;
		public readonly static int PORTAL     = 4;

		private int type = Obstacle.UNKNOWN;

		public int getType()
		{
			return type;
		}

		public void setType(int type)
		{
			this.type = type;
		}
	}
}