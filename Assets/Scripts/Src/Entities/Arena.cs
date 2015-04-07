using Observers;
using System.Collections.Generic;

namespace Entities
{
	public class Arena : Base
	{
		private List<Player> players;
		private Timer timer;

		public Arena ()
		{
			base.setTag ("Arena");
			this.players = new List<Player> (4);
		}

		public void addPlayer(Player player)
		{
			players.Add(player);
		}

		public Player getPlayer(int player)
		{
			return players[player];
		}

		public List<Player> getAllPlayers()
		{
			return players;
		}
	}
}