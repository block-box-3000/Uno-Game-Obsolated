using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno_Game.Models.Cards.Uno.Expansion_Pack.Reverse_pack
{
    internal class ReverseSkipCard : Card
    {
        public ReverseCard reverseCard { get; }
        public SkipCard skipCard { get; }
        public ReverseSkipCard(int color, int points = 20, int turnsSkipped = 1) : base(color, points)
        {
            reverseCard = new ReverseCard(color, points);
            skipCard = new SkipCard(color, points, turnsSkipped);
        }

        public override string ToString()
        {
            return string.Concat(EnumCollections.getColorName(color), " reverse skip card");
        }
    }
}
