using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno_Game.Models;

namespace Uno_Game.Models.Cards.Uno
{
    public class SkipCard : Card
    {
        public int turnsSkipped { get; }

        public SkipCard(int cardColor, int points = 20, int turnsSkipped = 1) : base(cardColor, points)
        {
            this.turnsSkipped = turnsSkipped;
        }

        public override string ToString()
        {
            return string.Concat(EnumCollections.getColorName(color), " skip card");
        }
    }
}
