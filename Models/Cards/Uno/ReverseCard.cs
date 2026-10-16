using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uno_Game.Models;

namespace Uno_Game.Models.Cards.Uno
{
    public class ReverseCard : Card
    {
        public ReverseCard(int cardColor, int points = 20) : base(cardColor, points) 
        {
            
        }

        public override string ToString()
        {
            return string.Concat(EnumCollections.getColorName(color), " reverse card");
        }
    }
}
