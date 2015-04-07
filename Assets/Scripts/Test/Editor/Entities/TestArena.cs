using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

using Entities;

namespace TickingArenaTests
{
	[TestFixture]
	[Category("Arena Entity Tests")]
	internal class TestArena : MonoBehaviour
	{
		private Arena arena { set; get; }
		
		[Test]
		[Category("Arena Entity Exists")]
		public void ArenaExistsTest()
		{
			arena = new Arena ();

			if (arena.getTag().Equals("Arena"))
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}

		[Test]
		[Category("Arena Has No Players On Start")]
		public void EmptyPlayersOnStartTest()
		{
			List<Player> players = arena.getAllPlayers ();
			
			if (players.Count <= 0)
			{
				Assert.Pass();
			}
			else
			{
				Assert.Fail();
			}
		}
	}
}