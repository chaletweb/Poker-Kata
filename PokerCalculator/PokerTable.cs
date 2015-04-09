using System;
using System.Collections.Generic;
using System.Linq;
using PokerCalculator.Engine;
using PokerCalculator.Hand;
using PokerCalculator.Statistic;

namespace PokerCalculator
{
    public class PokerTable
    {
        public List<Player> Players { get; private set; }
        public Board Board { get; private set; }
        public IDealer Dealer { get; private set; }
        public IHandEngine HandEngine { get; private set; }
        
        public PokerTable(IDealer dealer, IHandEngine engine)
        {
            Players = new List<Player>();
            Board = new Board();
            HandEngine = engine;
            Dealer = dealer;
        }

        public PokerTable() : this(new Dealer(), new TexasHoldemEngine())
        {
            
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public int PlayerCount()
        {
            return Players.Count;
        }

        public IHand HandOfPlayer(Player player)
        {
            return HandEngine.HandValue(player.Cards, Board);
        }

        public List<Player> WhoWin()
        {
            var handList = Players.Select(x => Tuple.Create(x, HandOfPlayer(x).SelectedCards.Sum(y=>y.Value)))
                .OrderByDescending(x => x.Item2).ToList();

            var handGroup =
                handList.GroupBy(x => x.Item2)
                .ToList();

            return handGroup[0].ToList().Select(x=>x.Item1).ToList();
        }

        public Dictionary<Player, double> Statistics()
        {
            StatisticsEngine statistiqueEngine = new StatisticsEngine(this);
            return statistiqueEngine.StatisticsPerPlayer();
        }
        
    }
}