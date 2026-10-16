using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno_Game.Models;

namespace Uno_Game.Models.Cards.Uno
{
    public class DrawWildCard : DrawCard
    {
        public DrawWildCard(int turnsSkipped = 1, int cardCant = 4) : 
            base((int) EnumCollections.cardColors.WILDCARD, 50, turnsSkipped, cardCant)
        {

        }

        override
        public string ToString()
        {
            return "Draw "+ cardCant + " Wild card";
        }
    }
}
