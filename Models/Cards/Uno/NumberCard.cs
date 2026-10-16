using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno_Game.Models;

namespace Uno_Game.Models.Cards.Uno
{
    public class NumberCard : Card
    {
        public int number { get; }
        public NumberCard(int color, int points, int number) : base(color, points)
        {
            this.number = number;
        }

        public override string ToString()
        {
            string text = string.Concat(number, " ", EnumCollections.getColorName(color), " card");
            return text;
        }
    }
}
