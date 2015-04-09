using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandBrelan : HandBase
    {
        public HandBrelan()
        {
            HandType = HandType.Brelan;
        }
        public override string ToString()
        {
            return "Brelan de " + SelectedCards.First();
        }
        public override IHand Check(List<Card> cards)
        {
            HandBase hand = null;
            var groupCard =
                cards.GroupBy(x => x.Value)
                    .OrderByDescending(group => @group.Count())
                    .ThenByDescending(x => x.Key)
                    .Select(group => Tuple.Create(@group, @group.Count()))
                    .ToList();

            if (groupCard[0].Item2 == 3)
            {
                hand = new HandBrelan();
                hand.SelectedCards.AddRange(groupCard[0].Item1.ToList());

                for (int i = 1; i < groupCard.Count && i <= 2; i++)
                {
                    hand.SelectedCards.Add(groupCard[i].Item1.ToList()[0]);
                }
            }


            return hand;
        }
    }
}