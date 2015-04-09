using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Hand;

namespace PokerCalculator.Engine
{
    public class OmahaEngine : HandEngine
    {
        public override IHand HandValue(List<Card> playerCards, Board board)
        {
            var handList = new List<IHand>();
            var allCombineOfPlayerCards = PermuteUtils.Combine(playerCards, 2);
            var allCombineOfBoard = PermuteUtils.Combine(board.Cards, 3);

            foreach (var combineOfPlayerCard in allCombineOfPlayerCards)
            {
                foreach (var combineOfBoard in allCombineOfBoard)
                {
                    var hand = base.HandValue(combineOfPlayerCard.ToList(), new Board(){Cards = combineOfBoard.ToList() });
                    handList.Add(hand);
                }
            }

            return handList.Max();
        }
    }
}
