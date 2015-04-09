using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandBase : IHand, IHandChecker
    {
        public HandType HandType { get; set;}
        public List<Card> SelectedCards { get; set; }
        public virtual IHand Check(List<Card> cards)
        {
            HandBase hand = new HandBase();

            hand.SelectedCards.AddRange(cards.OrderByDescending(x => x.Value).Take(5));

            return hand;
        }
        public HandBase()
        {
            SelectedCards = new List<Card>(5);
            HandType = HandType.Hauteur;
        }
        public int CompareTo(IHand other)
        {
            if (GetType() != other.GetType())
            {
                return HandType > other.HandType ? 1 : -1;
            }

            for (int i = 0; i < SelectedCards.Count; i++)
            {
                if (other.SelectedCards.Count > i)
                {
                    if(SelectedCards[i].Value > other.SelectedCards[i].Value)
                    {
                        return 1;
                    }
                    if (SelectedCards[i].Value < other.SelectedCards[i].Value)
                    {
                        return -1;
                    }
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }
        public bool Equals(IHand other)
        {
            return CompareTo(other) == 0;
        }
        public override string ToString()
        {
               return "Hauteur " + SelectedCards.First();  
        }
        protected static void AddOneIfAs(List<Card> groupCard)
        {
            var cardAs = groupCard.FirstOrDefault(x => x.Value == 14);

            if (cardAs == null) return;
            var carUn = new Card(1, cardAs.Color);
            groupCard.Add(carUn);
        }
    }
}