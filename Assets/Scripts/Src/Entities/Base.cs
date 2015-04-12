namespace Entities
{
	[System.Serializable]
	public abstract class Base
	{
		[UnityEngine.SerializeField]
		private int _id      = -1;

		[UnityEngine.SerializeField]
		private string _name = "";

		[UnityEngine.SerializeField]
		private string _tag  = "";

		public Base()
		{
		}

		public Base(int id)
		{
			this._id = id;
		}

		public Base(int id, string name)
		{
			this._id = id;
			this._name = name;
		}

		public Base(int id, string name, string tag)
		{
			this._id = id;
			this._name = name;
			this._tag = tag;
		}

		public int Id
		{
			set { this._id = value; }
			get { return this._id; }
		}

		public string Name
		{
			set { this._name = value; }
			get { return this._name; }
		}

		public string Tag
		{
			set { this._tag = value; }
			get { return this._tag; }
		}
	}
}
