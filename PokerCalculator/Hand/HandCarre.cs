using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandCarre : HandBase
    {
        public HandCarre()
        {
            HandType = HandType.Carre;
        }
        public override string ToString()
        {
            return "Carre de " + SelectedCards.First();
        }
        public override IHand Check(List<Card> cards)
        {
            var groupCard =
                cards.GroupBy(x => x.Value)
                    .OrderByDescending(group => @group.Count())
                    .ThenByDescending(x => x.Key)
                    .Select(group => Tuple.Create(@group, @group.Count()))
                    .ToList();

            if (groupCard[0].Item2 != 4) return null;

            HandBase hand = new HandCarre();
            hand.SelectedCards.AddRange(groupCard[0].Item1.ToList());
            if (groupCard.Count >= 2)
            {
                hand.SelectedCards.Add(groupCard[1].Item1.First());
            }
            return hand;
        }
    }
}