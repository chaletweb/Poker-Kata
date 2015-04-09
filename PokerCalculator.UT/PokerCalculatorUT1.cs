using NFluent;
using NUnit.Framework;
using PokerCalculator.Engine;

namespace PokerCalculator.UT
{
    public class PokerCalculatorUt1
    {
        private PokerTable _pokerTable;
        private IDealer _dealer;

        private Player _player1;
        private Player _player2;

        [SetUp]
        public void SetUp()
        {
            _pokerTable = new PokerTable();
            _dealer = _pokerTable.Dealer;
            _player1 = new Player("Player1");
            _pokerTable.AddPlayer(_player1);
            _player2 = new Player("Player2");
            _pokerTable.AddPlayer(_player2);
        }

        [Test]
        public void ShouldCount2PlayersWhen2PlayersPlay()
        {
            Check.That(_pokerTable.PlayerCount()).Equals(2);
        }

        [Test]
        public void ShouldUseTexasHoldemEngineWhenEngineIsNotDefined()
        {
            Check.That(_pokerTable.HandEngine.GetType()).Equals(typeof(TexasHoldemEngine));
        }

        
    }
} 