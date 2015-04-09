using System.Collections.Generic;
using PokerCalculator.Hand;

namespace PokerCalculator.Engine
{
    public interface IHandEngine
    {
        int BoardCardCount { get; }

        int PlayerCardsCount { get;}

        IHand HandValue(List<Card> playerCards, Board board);
    }
}