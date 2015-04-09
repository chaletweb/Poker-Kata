using System.Collections.Generic;

namespace PokerCalculator
{
    public interface IBoard
    {
        List<Card> Cards { get; }
        void ClearCards();
        void AddCard(Card card);
    }
}