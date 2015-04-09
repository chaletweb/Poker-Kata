using System.Collections.Generic;

namespace PokerCalculator
{
    public class Player
    {
        public string Name { get; private set; }
        public List<Card> Cards { get; private set; }

        public Player(string playerName)
        {
            Name = playerName;
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
