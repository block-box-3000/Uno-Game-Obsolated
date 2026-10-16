using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno_Game.Models.Cards.Uno.Expansion_Pack.Reverse_pack
{
    public class ReverseDrawCard : Card
    {
        public DrawCard drawCard { get; }
        public ReverseCard reverseCard { get; }
        public ReverseDrawCard(int cardColor, int points = 20, int turnsSkipped = 1, int cardCant = 2) : base(cardColor, points)
        {
            drawCard = new DrawCard(cardColor, points, turnsSkipped, cardCant);
            reverseCard = new ReverseCard(cardColor, points);
        }

        override
        public string ToString()
        {
            string text = EnumCollections.getColorName(color);

            text += " reverse draw 2 cards";

            return text;
        }
    }
}
