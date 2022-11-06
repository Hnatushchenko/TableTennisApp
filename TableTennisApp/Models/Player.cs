using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Player
    {
        public string Name { get; set; }
        public int TotalGames { get; set; }
        public Player(int totalGames)
        {
            TotalGames =totalGames;
            
        }
        public Player(string name)
        {
            Name = name;
          
        }
    }
}
