using System;
using System.Collections.Generic;

namespace PokerCalculator
{
    public class Board : IBoard, ICloneable
    {
        public List<Card> Cards { get; set; }

        public Board()
        {
            Cards = new List<Card>();
        }

        public void ClearCards()
        {
            Cards.Clear();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public object Clone()
        {
            Board cloneBoard = new Board();
            cloneBoard.Cards.AddRange(Cards);
            return cloneBoard;
        }

        public void AddCards(List<Card> cards)
        {
            Cards.AddRange(cards);
        }
    }
}
