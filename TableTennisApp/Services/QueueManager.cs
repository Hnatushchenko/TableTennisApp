using TableTennisApp.Models;

namespace TableTennisApp.Services
{
    public class QueueManager : IQueueManager
    {
        private static List<Player> Players = new List<Player>();
        private readonly IPlayersService _playersService;

        public QueueManager(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return Players;
        }
        public void AddPlayer(Player player)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Id == player.Id)
                {
                    return;
                }
            }
            Players.Add(player);
        }
        public void RemoveFirst()
        {
            Players.RemoveAt(0);
        }
        public void RemovePlayer(Player player)
        {
            Player P = Players.First(p => p.Id == player.Id);
            Players.Remove(P);
        }
        public void PutToEnd(Player player)
        {
            foreach (var playerInQueue in Players)
            {
                if (playerInQueue.Id == player.Id)
                {
                    Players.Remove(playerInQueue);
                }
            }
            Players.Add(player);
        }
        public void RemovePlayerByLogin(string login)
        {
            Player? playerToRemove = Players.FirstOrDefault(p => p.Login == login);
            if (playerToRemove is not null)
            {
                Players.Remove(playerToRemove);
            }
        }
        public void AddPlayerByLogin(string login)
        {
            Player? playerToAdd = _playersService.GetByLogin(login);
            if (playerToAdd is null)
            {
                throw new ArgumentException("Invalid login");
            }

            AddPlayer(playerToAdd);
        }
    }
}
