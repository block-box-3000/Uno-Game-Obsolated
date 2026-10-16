namespace Uno_Game.Models.Cards.Uno.Expansion_Pack.Stack_pack
{
    public class WildStackCard : StackCard
    {
        public WildStackCard(int points = 50, int turnsSkipped = 1, int cardCant = 3) : 
            base((int) EnumCollections.cardColors.WILDCARD, points, turnsSkipped, cardCant)
        {
            
        }

        public override string ToString()
        {
            return string.Concat("Wild stack ",cardCant," Card");
        }
    }
}
