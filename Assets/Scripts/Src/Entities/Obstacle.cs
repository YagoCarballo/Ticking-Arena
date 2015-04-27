namespace Entities
{
	public enum ObstacleType { 
		UNKNOWN		 = -1, 
		WALL		 = 0, 
		FLOOR		 = 1, 
		CEILING		 = 2, 
		PLATFORM	 = 3, 
		PORTAL		 = 4
	}

	[System.Serializable]
	public class Obstacle : Base
	{
		[UnityEngine.SerializeField]
		private ObstacleType _type = ObstacleType.UNKNOWN;

		public Obstacle ()
		{
			base.Tag = "Obstacle";
		}

		public ObstacleType getType()
		{
			return _type;
		}

		public void setType(ObstacleType type)
		{
			this._type = type;
		}
	}
}