using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PokerCalculator.Hand;

namespace PokerCalculator.Engine
{
    public abstract class HandEngine : IHandEngine
    {
        public int BoardCardCount { get; private set; }

        public int PlayerCardsCount { get; private set; }
        

        protected HandEngine()
        {
            var className = this.GetType().Name;
            XElement xmlConfigFile = XElement.Load(string.Format(@".\Config\{0}.xml", className));
            var playerCardsCount = Int32.Parse(xmlConfigFile.Elements().FirstOrDefault(x => x.Name == "PlayerCardsCount").Value);
            var boardCardsCount = Int32.Parse(xmlConfigFile.Elements().FirstOrDefault(x => x.Name == "BoardCardsCount").Value);
            BoardCardCount = boardCardsCount;
            PlayerCardsCount = playerCardsCount;
        }

        public virtual IHand HandValue(List<Card> playerCards, Board board)
        {
            List<Card> cards = new List<Card>();
            cards.AddRange(playerCards);
            cards.AddRange(board.Cards);

            var listOfPossibleHand = new List<IHandChecker>
            {
                new HandStraightFlush(),
                new HandCarre(),
                new HandFull(),
                new HandFlush(),
                new HandStraight(),
                new HandBrelan(),
                new HandDoublePair(),
                new HandPair(),
                new HandBase()
            };

            if (cards.Count < 1)
            {
                return null;
            }

            return listOfPossibleHand.Select
                (possibleHand => possibleHand.Check(cards))
                .FirstOrDefault(hand => hand != null);
        }
    }
}
