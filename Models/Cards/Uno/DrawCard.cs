using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno_Game.Models;

namespace Uno_Game.Models.Cards.Uno
{
    public class DrawCard : SkipCard
    {
        public int cardCant { get; }
        public DrawCard(int cardColor, int points = 20, int turnsSkipped = 1, int cardCant = 2) : base(cardColor, points, turnsSkipped) 
        {
            this.cardCant = cardCant;
        }

        override
        public string ToString()
        {
            string text = EnumCollections.getColorName(color);

            text += " draw 2 cards";

            return text;
        }
    }
}
