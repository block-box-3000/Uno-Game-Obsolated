using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uno_Game.Models
{
    public abstract class Card
    {

        public int color { get; set; }
        public int points { get; }

        public Card(int color, int points)
        {
            this.color = color;
            this.points = points;
        }
    }
}
