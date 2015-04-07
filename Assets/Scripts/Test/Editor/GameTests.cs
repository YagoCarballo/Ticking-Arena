using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;

namespace TickingArenaTests
{
	[TestFixture]
	[Category("Game Tests")]
	internal class GameTests : MonoBehaviour
	{
		[Test]
		[Category("Main Test")]
		public void ExceptionTest()
		{
			Assert.Pass();
		}
	}
}