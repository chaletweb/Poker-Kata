using System.Collections.Generic;

namespace PokerCalculator.Hand
{
    public interface IHandChecker
    {
        IHand Check(List<Card> cards);
    }
}
