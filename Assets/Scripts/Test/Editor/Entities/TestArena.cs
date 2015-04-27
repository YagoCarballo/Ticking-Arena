using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

using Entities;

namespace TickingArenaTests
{
	namespace EntitiesTests
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
				Assert.AreEqual ("Arena", arena.Tag);
			}
			
			[Test]
			[Category("Arena Has No Players On Start")]
			public void EmptyPlayersOnStartTest()
			{
				List<Player> players = arena.getAllPlayers ();
				Assert.LessOrEqual (players.Count, 0);
			}
			
			[Test]
			[Category("Players can be added and fetched from the arena")]
			public void PlayersCanBeAdded()
			{
				Player player	= new Player ();
				player.Name		= "John Doe";
				
				arena.addPlayer (player);
				Assert.AreSame (arena.getPlayer (0), player);
			}
			
			[Test]
			[Category("Arena has a property Timer and is not null")]
			public void TimerIsAccessible()
			{
				Entities.Timer timer = arena.Timer;
				Assert.NotNull (timer);
			}
		}
	}
}