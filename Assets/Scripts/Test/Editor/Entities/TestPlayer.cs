using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

using Entities;
using Game.InputDevices;

namespace TickingArenaTests
{
	namespace EntitiesTests
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
				Assert.AreEqual ("Player", player.Tag);
			}

			[Test]
			[Category("Player Entity Has a Color Property")]
			public void PlayerHasColorProperty()
			{
				player.Colour = PlayerColour.Red;
				Assert.AreEqual (PlayerColour.Red, player.Colour);
			}

			[Test]
			[Category("Player Entity Has a Gender Property")]
			public void PlayerHasGenderProperty()
			{
				player.Gender = PlayerGender.Female;
				Assert.AreEqual (PlayerGender.Female, player.Gender);
			}

			[Test]
			[Category("Player Entity Generates proper Sprite Name")]
			public void PlayerGeneratesName()
			{
				player.Colour = PlayerColour.Green;
				player.Gender = PlayerGender.Male;

				Assert.AreEqual ("MalePlayer-Green", player.getSpriteName());
			}

			[Test]
			[Category("Player Entity has time Properties")]
			public void PlayerHasTimeProperties()
			{
				player.TimeLeft = 0.25f;
				player.TotalTime = 0.5f;
				Assert.AreEqual (0.25f, player.TimeLeft);
				Assert.AreEqual (0.50f, player.TotalTime);
			}

			[Test]
			[Category("Player Entity has Input Type Properties")]
			public void PlayerHasInputTypeProperty()
			{
				player.InputInfo = new Game.InputDevices.InputInfo(0, typeof(KeyboardInputDevice), true);
				Assert.NotNull (player.InputInfo);
			}
	}
	}
}