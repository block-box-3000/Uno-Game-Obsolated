namespace Uno_Game.Models.Cards.Uno.Expansion_Pack.Stack_pack
{
    public class StackCard : Card
    {
        public static int stack = 0;
        public int turnsSkipped {  get; }
        public int cardCant {  get; }
        public StackCard(int cardColor, int points = 20, int turnsSkipped = 1, int cardCant = 1) : base(cardColor, points)
        {
            this.turnsSkipped = turnsSkipped;
            this.cardCant = cardCant;
        }

        public override string ToString()
        {
            return string.Concat(EnumCollections.getColorName(color), " stack ",cardCant," card(s)");
        }
    }
}
