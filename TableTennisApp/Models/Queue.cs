namespace TableTennisApp.Models
{
    public class Queue
    {
        public List <Player> Players = new List<Player>();
        public void AddPlayer(Player player)
        {
            
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Name == player.Name)
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
            Player P=Players.First(p=>p.Name==player.Name);
            Players.Remove(P);
        }
        public void PutToEnd(Player player)
        {
            foreach (var playerInQueue in Players)
            {
                if (playerInQueue.Id == player.Id)
                {
                    RemovePlayer(playerInQueue);
                }
            }
            Players.Add(player);
        }
    }
}
