namespace Uno_Game.Models.Cards.Uno.Expansion_Pack.Stack_pack
{
    public class WildStackNumberCard : StackCard
    {
        public WildStackNumberCard(int points = 50, int turnsSkipped = 1, int cardCant = 0) : 
            base((int) EnumCollections.cardColors.WILDCARD, points, turnsSkipped, cardCant)
        {
            
        }

        public override string ToString()
        {
            return string.Concat("Wild Stack Number Card");
        }
    }
}
