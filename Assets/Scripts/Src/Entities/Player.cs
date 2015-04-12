using Observers;
using UnityEngine;
using Game.InputDevices;

namespace Entities
{
	public enum PlayerColour { 
		Gray	= 0, 
		Red		= 1, 
		Green	= 2, 
		Blue	= 3, 
		Yellow	= 4
	}

	public enum PlayerGender { 
		Male	= 1, 
		Female	= 2
	}

	[System.Serializable]
	public class Player : Base, TimerObserver
	{
		[UnityEngine.SerializeField]
		private PlayerColour _colour;

		[UnityEngine.SerializeField]
		private PlayerGender _gender;

		private InputInfo _inputInfo;
		private float timeLeft;
		private float totalTime;

		public readonly static Vector2[] FemaleCollisionPoints = {
			new Vector2(-0.1301057f,	0.1653351f),
			new Vector2(0.09389525f,	0.2309537f),
			new Vector2(0.1685975f,		0.3330927f),
			new Vector2(0.2254916f,		0.1334473f),
			new Vector2(0.2227041f,		-0.0006798506f),
			new Vector2(0.1693378f,		-0.213273f),
			new Vector2(0.03702725f,	-0.3438923f),
			new Vector2(0.05545247f,	-0.4263031f),
			new Vector2(0.125132f,		-0.4667439f),
			new Vector2(0.08871916f,	-0.5050802f),
			new Vector2(0.07145425f,	-0.6237379f),
			new Vector2(0.01943735f,	-0.6511045f),
			new Vector2(0.01350193f,	-0.7331903f),
			new Vector2(0.0736149f,		-0.8125905f),
			new Vector2(-0.07435082f,	-0.8114422f),
			new Vector2(-0.08069408f,	-0.6558107f),
			new Vector2(-0.1387083f,	-0.6246806f),
			new Vector2(-0.1107327f,	-0.4271892f),
			new Vector2(-0.06018394f,	-0.3618971f),
			new Vector2(-0.1353243f,	-0.3016625f),
			new Vector2(-0.1715029f,	-0.3273836f),
			new Vector2(-0.2214135f,	-0.1255843f),
			new Vector2(-0.1954238f,	0.09887075f)
		};

		public readonly static Vector2[] MaleCollisionPoints = {
			new Vector2(-0.1755205f,	0.3098364f),
			new Vector2(0.04435188f,	0.3382975f),
			new Vector2(0.2016263f,		0.382636f),
			new Vector2(0.262649f,		0.1499619f),
			new Vector2(0.2681187f,		-0.04609463f),
			new Vector2(0.1775949f,		-0.3123597f),
			new Vector2(0.08657061f,	-0.368664f),
			new Vector2(0.05545247f,	-0.4263031f),
			new Vector2(0.125132f,		-0.4667439f),
			new Vector2(0.08871916f,	-0.5050802f),
			new Vector2(0.07145425f,	-0.6237379f),
			new Vector2(0.01943735f,	-0.6511045f),
			new Vector2(0.01350193f,	-0.7331903f),
			new Vector2(0.06908852f,	-0.7944849f),
			new Vector2(-0.07585964f,	-0.7933365f),
			new Vector2(-0.08069408f,	-0.6558107f),
			new Vector2(-0.1345797f,	-0.5833945f),
			new Vector2(-0.1107327f,	-0.4271892f),
			new Vector2(-0.08495554f,	-0.3660257f),
			new Vector2(-0.1353243f,	-0.3016625f),
			new Vector2(-0.1715029f,	-0.195268f),
			new Vector2(-0.2709569f,	0.1592899f),
			new Vector2(-0.2408385f,	0.2433721f)
		};

		public Player ()
		{
			this.Tag = "Player";
		}

		public Player (int id) : this()
		{
			this.Id		= id;
			this.Name	= "Player_" + id;
		}

		public string getSpriteName()
		{
			string spriteGender = "Male";
			string spriteColour = "Gray";

			if (this._gender == PlayerGender.Female)
			{
				spriteGender = "Female";
			}

			switch (this._colour)
			{
			case PlayerColour.Red:
				spriteColour = "Red";
				break;
			case PlayerColour.Green:
				spriteColour = "Green";
				break;
			case PlayerColour.Blue:
				spriteColour = "Blue";
				break;
			case PlayerColour.Yellow:
				spriteColour = "Yellow";
				break;
			default:
				spriteColour = "Gray";
				break;
			}

			return spriteGender + "Player-" + spriteColour;
		}

		public float TimeLeft
		{
			set { this.timeLeft = value; }
			get { return this.timeLeft; }
		}

		public float TotalTime
		{
			set { this.totalTime = value; }
			get { return this.totalTime; }
		}

		public PlayerGender Gender
		{
			set { this._gender = value; }
			get { return this._gender; }
		}

		public PlayerColour Colour
		{
			set { this._colour = value; }
			get { return this._colour; }
		}

		public InputInfo InputInfo
		{
			set { this._inputInfo = value; }
			get { return this._inputInfo; }
		}

		public void timerEnded(float time, int player)
		{

		}

		public void timerStarted(float time, int player)
		{

		}

		public void timerChangedOwner(int newPlayer, int oldPlayer)
		{

		}
	}
}
