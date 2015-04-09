using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandFlush : HandBase
    {
        public HandFlush()
        {
            HandType = HandType.Couleur;
        }
        public override string ToString()
        {
            return "Couleur " + SelectedCards.First().Color; 
        }
        public override IHand Check(List<Card> cards)
        {
            HandBase hand = null;
            var groupCard =
                cards.GroupBy(x => x.Color)
                    .OrderByDescending(group => @group.Count())
                    .Select(group => Tuple.Create(@group, @group.Count()))
                    .ToList();

            if (groupCard[0].Item2 >= 5)
            {
                hand = new HandFlush();
                hand.SelectedCards.AddRange(groupCard[0].Item1.ToList().OrderByDescending(x => x.Value));
            }
            return hand;
        }
    }
}