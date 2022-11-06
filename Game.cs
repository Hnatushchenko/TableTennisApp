using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

    }

}
