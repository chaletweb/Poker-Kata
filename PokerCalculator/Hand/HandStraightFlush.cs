using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator.Hand
{
    public class HandStraightFlush : HandBase
    {
        public HandStraightFlush()
        {
            HandType = HandType.QuinteFlush;
        }
        public override string ToString()
        {
            return "Quinte Flush au " + SelectedCards.First();
        }
        public override IHand Check(List<Card> cards)
        {
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

                    var handGroup = groupCard.Where(x => x.Value >= cardMinValue && x.Value <= cardMaxValue).ToList();

                    var colorHand = new HandFlush();
                    if (colorHand.Check(handGroup) != null)
                    {
                        HandBase hand = new HandStraightFlush();
                        hand.SelectedCards.AddRange(handGroup);
                        return hand;
                    }
                }
            }

            return null; 
        }
    }
}