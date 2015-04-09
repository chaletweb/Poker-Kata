using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandDoublePair : HandBase
    {
        public HandDoublePair()
        {
            HandType = HandType.TwoPaire;
        }
        public override string ToString()
        {
            return "Paire de " + SelectedCards.First() + " et de " + SelectedCards[2];
        }
        public override IHand Check(List<Card> cards)
        {
            var groupCard =
                cards.GroupBy(x => x.Value)
                    .OrderByDescending(group => @group.Count()).ThenByDescending(group => @group.Key)
                    .Select(group => Tuple.Create(@group, @group.Count()))
                    .ToList();

            if (groupCard.Count < 2)
            {
                return null;
            }

            if (groupCard[0].Item2 != 2 || groupCard[1].Item2 != 2) return null;

            HandBase hand = new HandDoublePair();
            hand.SelectedCards.AddRange(groupCard[0].Item1.ToList());
            hand.SelectedCards.AddRange(groupCard[1].Item1.ToList());

            if (groupCard.Count >= 3)
            {
                hand.SelectedCards.Add(groupCard[2].Item1.ToList()[0]);
            }
            return hand;
        }
    }
}