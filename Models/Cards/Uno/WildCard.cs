using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno_Game.Models;

namespace Uno_Game.Models.Cards.Uno
{
    public class WildCard : Card
    {
        public WildCard() : base((int) EnumCollections.cardColors.WILDCARD, 50)
        {

        }

        public override string ToString()
        {
            return "Wild card";
        }
    }
}
