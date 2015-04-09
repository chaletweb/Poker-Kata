using System.Collections.Generic;
using PokerCalculator.Hand;

namespace PokerCalculator.Statistic
{
    public class CardPrevision
    {
        public Player PlayerWin { get; set; }
        public IHand CurrentHand { get; set; }
        public List<Card> PossibleCards { get; set; }

        public CardPrevision()
        {
            PossibleCards = new List<Card>();
        }
    }
}