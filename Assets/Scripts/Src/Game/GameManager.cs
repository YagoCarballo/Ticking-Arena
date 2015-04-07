using UnityEngine;
using System.Collections;

namespace Game
{
	public class GameManager
	{
		// Singleton
		private static GameManager instance;

		public static GameManager Instance ()
		{
			if (instance == null)
			{
				instance = new GameManager();
			}
			
			return instance;
		}
		
		// Game Elements
//		private Arena arena;
		
		private GameManager () {}
	}
}
