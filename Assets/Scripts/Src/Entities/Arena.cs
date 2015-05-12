using Observers;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
	[System.Serializable]
	public class Arena : Base
	{
		[UnityEngine.SerializeField]
		private List<Player> _players;

		public Arena ()
		{
			base.Tag = "Arena";
			this._players = new List<Player> (4);
		}

		public void addPlayer(Player player)
		{
			_players.Add(player);
		}

		public Player getPlayer(int player)
		{
			return _players[player];
		}

		public List<Player> getAllPlayers()
		{
			return _players;
		}
	}
}