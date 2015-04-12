using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

using Entities;

namespace TickingArenaTests
{
	[TestFixture]
	[Category("Player Entity Tests")]
	internal class TestPlayer : MonoBehaviour
	{
		private Player player { set; get; }

		[Test]
		[Category("Player Entity Exists")]
		public void PlayerExistsTest()
		{
			player = new Player ();

			if (player.Tag.Equals("Player"))
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