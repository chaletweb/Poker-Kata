using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandFull : HandBase
    {
        public HandFull()
        {
            HandType = HandType.Full;
        }
        public override string ToString()
        {
            return "Full de " + SelectedCards.First() + " par les " + SelectedCards[3];
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

            if (groupCard[0].Item2 != 3 || groupCard[1].Item2 < 2) return null;

            HandBase hand = new HandFull();
            hand.SelectedCards.AddRange(groupCard[0].Item1.ToList());
            hand.SelectedCards.AddRange(groupCard[1].Item1.Take(2).ToList());
            return hand;
        }
    }
}