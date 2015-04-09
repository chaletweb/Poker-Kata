using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandPair : HandBase
    {
        public HandPair()
        {
            HandType = HandType.Paire;
        }
        public override string ToString()
        {
            return "Paire de " + SelectedCards.First();
        }
        public override IHand Check(List<Card> cards)
        {
            HandBase hand = null;
            var groupCard =
                cards.GroupBy(x => x.Value)
                    .OrderByDescending(group => @group.Count()).ThenByDescending(x => x.Key)
                    .Select(group => Tuple.Create(@group, @group.Count()))
                    .ToList();

            if (groupCard.Count < 1)
            {
                return null;
            }

            if (groupCard[0].Item2 == 2)
            {
                hand = new HandPair();
                hand.SelectedCards.AddRange(groupCard[0].Item1.ToList());

                for (int i = 1; i < groupCard.Count && i <= 3; i++)
                {
                    hand.SelectedCards.Add(groupCard[i].Item1.ToList()[0]);
                }
            }

            return hand;
        }
    }
}