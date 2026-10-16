using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno_Game.Models
{
    public class Player
    {
        int ID { get; }
        public List<Card> hand { get; set; }

        public Player(int id, List<Card> hand)
        {
            ID = ID;
            this.hand = hand;
        }
    }
}
