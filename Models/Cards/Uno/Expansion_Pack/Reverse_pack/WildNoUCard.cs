using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno_Game.Models.Cards.Uno.Expansion_Pack.Reverse_pack
{
    public class WildNoUCard : Card
    {
        public WildNoUCard() : base((int) EnumCollections.cardColors.WILDCARD, 50)
        {
        }

        public override string ToString()
        {
            return "Wild No U Card";
        }
    }
}
