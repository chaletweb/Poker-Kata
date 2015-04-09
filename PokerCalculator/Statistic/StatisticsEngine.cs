using System;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Engine;
using PokerCalculator.Hand;
using System.Threading.Tasks;

namespace PokerCalculator.Statistic
{
    public class StatisticsEngine
    {
        private PokerTable Table { get; set; }
        public StatisticsEngine(PokerTable pokerTable)
        {
            Table = pokerTable;
        }

        public Dictionary<Player, double> StatisticsPerPlayer()
        {
            Dictionary<Player, double> stat = new Dictionary<Player, double>();
            var previsions = Previsions();

            foreach (var playerStat in previsions.GroupBy(x => x.PlayerWin).ToList())
            {
                double perc = playerStat.ToList().Count / (double)previsions.Count;
                stat.Add(playerStat.Key, perc);
            }

            return stat;
        }

        public List<CardPrevision> Previsions()
        {
            List<CardPrevision> cardPrevisions = new List<CardPrevision>();

            if (Table.Players.Any(player => player.Cards.Count != Table.HandEngine.PlayerCardsCount))
            {
                return null;
            }

            int missingCountBoardCardCount = Table.HandEngine.BoardCardCount - Table.Board.Cards.Count;

            var possibleCardsForTheBoard = PermuteUtils.Combine(Table.Dealer.CardPack, missingCountBoardCardCount);

            var taskList = new List<Task>();
            foreach (var cards in possibleCardsForTheBoard)
            {
                Task task = Task.Factory.StartNew(() => 
                    {
                        var cp = CardPrevisionCalculator(cards);
                        if (cp != null)
                            cardPrevisions.AddRange(cp);
                    }
                    );
                taskList.Add(task);
            }
            Task.WaitAll(taskList.ToArray());
            return cardPrevisions;
        }

        private IEnumerable<CardPrevision> CardPrevisionCalculator(IEnumerable<Card> cards)
        {
            List<CardPrevision> cardPrevisions = new List<CardPrevision>();
            List<Tuple<Player, IHand, List<Card>>> playerHandList = new List<Tuple<Player, IHand, List<Card>>>();

            Board temporaryBoard = Table.Board.Clone() as Board;
            temporaryBoard.AddCards(cards.ToList());

            try
            {
                foreach (var player in Table.Players)
                {
                    var playerHandValue = Table.HandEngine.HandValue(player.Cards, temporaryBoard);
                    var item = Tuple.Create(player, playerHandValue, cards.ToList());
                    playerHandList.Add(item);
                }

                var cardPrevision = new CardPrevision();
                var tupleList = playerHandList.OrderByDescending(x => x.Item2).ToList();
                if (!tupleList[0].Item2.Equals(tupleList[1].Item2))
                {
                    var tuple = tupleList[0];
                    cardPrevision.PlayerWin = tuple.Item1;
                    cardPrevision.CurrentHand = tuple.Item2;
                    cardPrevision.PossibleCards.AddRange(tuple.Item3);
                    cardPrevisions.Add(cardPrevision);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return cardPrevisions;
        }
    }
}