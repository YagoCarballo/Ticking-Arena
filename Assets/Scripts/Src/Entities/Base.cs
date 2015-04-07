namespace Entities
{
	public abstract class Base
	{
		private int id      = -1;
		private string name = "";
		private string tag  = "";

		public Base()
		{
		}

		public Base(int id)
		{
			this.id = id;
		}

		public Base(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public Base(int id, string name, string tag)
		{
			this.id = id;
			this.name = name;
			this.tag = tag;
		}

		public int getId()
		{
			return id;
		}

		public void setId(int id)
		{
			this.id = id;
		}

		public string getName()
		{
			return name;
		}

		public void setName(string name)
		{
			this.name = name;
		}

		public string getTag()
		{
			return tag;
		}

		public void setTag(string tag)
		{
			this.tag = tag;
		}
}
}
