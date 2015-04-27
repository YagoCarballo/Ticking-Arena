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
		[Category("Portal Entity Tests")]
		internal class TestPortal : MonoBehaviour
		{
			private Portal portal { set; get; }

			[Test]
			[Category("Portal Entity Exists")]
			public void PortalExistsTest()
			{
				portal = new Portal ();
				Assert.AreEqual ("Portal", portal.Tag);
			}

			[Test]
			[Category("Portal Is Enabled on Start")]
			public void PortalIsEnabledTest()
			{
				Assert.IsTrue (portal.Active);
			}

			[Test]
			[Category("Portal Waiting Time is at least 1 second")]
			public void PortalWaitingTimeTest()
			{
				Assert.GreaterOrEqual (portal.WaitingTime, 1.0f);
			}
		}
	}
}