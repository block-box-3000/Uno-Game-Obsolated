using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Uno_Game.Models
{
    public static class EnumCollections
    {
        public enum cardColors
        {
            RED = 0,
            YELLOW = 1,
            BLUE = 2,
            GREEN = 3,
            WILDCARD = 4
        }

        public enum difficulty
        {
            EASY = 0,
            NORMAL = 1,
            HARD = 2,
            VERY_HARD = 3
        }

        public enum gameStates
        {
            WAITING = 0,
            INGAME = 1,
            GAMEOVER = 2
        }

        public enum gameModes
        {
            NORMAL = 0
        }

        [Flags]
        public enum gamePacks
        {
            NORMAL = 0,
            REVERSE = 1 << 0,
            STACK = 1 << 1,
            SWAP = 1 << 2,
            SPEED = 1 << 3,
            FRIENEMIES = 1 << 4,
            DEFENSE = 1 << 5,
            COMEBACK = 1 << 6,
            BILLIE = 1 << 7,
            NO_MERCY = 1 << 8
        }

        public static string getColorName(int color)
        {
            string colorName = "";

            switch (color)
            {
                case (int) cardColors.RED:
                    colorName += "Red";
                    break;
                case (int) cardColors.YELLOW:
                    colorName += "Yellow";
                    break;
                case (int) cardColors.BLUE:
                    colorName += "Blue";
                    break;
                case (int) cardColors.GREEN:
                    colorName += "Green";
                    break;
            }

            return colorName;
        }
    }
}
