using NUnit.Framework;

namespace PokerCalculator.UT
{
    class PokerStatisticsTests
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
        public void FirstStatisticTest()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(7, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Coeur));

            _pokerTable.Dealer.GiveCardToBoard(_pokerTable.Board, new Card(CardValue.Valet, ColorCard.Pique));

            var d = _pokerTable.Statistics();
        }

        [Test]
        public void SecondStatisticTest()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(7, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Coeur));

            _pokerTable.Dealer.GiveCardToBoard(_pokerTable.Board, 2);

            var d = _pokerTable.Statistics();
        }
    }
}
