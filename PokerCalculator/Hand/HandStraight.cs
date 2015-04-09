using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandStraight : HandBase
    {
        public HandStraight()
        {
            HandType = HandType.Quinte;
        }
        public override string ToString()
        {
            return "Quinte au " + SelectedCards.First();
        }
        public override IHand Check(List<Card> cards)
        {
            HandBase hand = null;

            if (cards.Count < 5)
                return null;

            var groupCard =
                cards.GroupBy(x => x.Value)
                    .Select(group => @group.ToList()[0])
                    .OrderByDescending(x => x.Value)
                    .ToList();

            AddOneIfAs(groupCard);

            for (int i = 0; i + 5 <= groupCard.Count; i++)
            {
                if (groupCard.ToList()[i].Value -
                    groupCard.ToList()[i + 4].Value == 4)
                {
                    int cardMinValue = groupCard.ToList()[i + 4].Value;
                    int cardMaxValue = groupCard.ToList()[i].Value;
                    hand = new HandStraight();
                    hand.SelectedCards.AddRange(groupCard.Where(x => x.Value >= cardMinValue && x.Value <= cardMaxValue).ToList());

                }
                if (hand != null)
                    return hand;
            }

            return null; 
        }
    }
}