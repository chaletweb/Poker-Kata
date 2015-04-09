using NFluent;
using NUnit.Framework;
using PokerCalculator.Engine;

namespace PokerCalculator.UT
{
    public class OmahaPokerTest
    {
        private PokerTable _pokerTable;
        private IDealer _dealer;

        private Player _player1;
        private Player _player2;

        [SetUp]
        public void SetUp()
        {
            _dealer = new Dealer();
            _pokerTable = new PokerTable(_dealer, new OmahaEngine());

            _player1 = new Player("Player1");
            _pokerTable.AddPlayer(_player1);
            _player2 = new Player("Player2");
            _pokerTable.AddPlayer(_player2);
        }

        [Test]
        public void ShouldReturnBrelanAsWhenPlayerHasThisHand()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Carreau));
            _dealer.GiveCardToPlayer(_player1, new Card(CardValue.As, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(CardValue.As, ColorCard.Carreau));

            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(11, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(CardValue.As, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(CardValue.Roi, ColorCard.Coeur));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Brelan de As");
        }

        [Test]
        public void ShouldReturnFullAsWhenPlayerHasThisHand()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Carreau));
            _dealer.GiveCardToPlayer(_player1, new Card(CardValue.As, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(CardValue.As, ColorCard.Carreau));

            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Pique));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(CardValue.As, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(CardValue.Roi, ColorCard.Coeur));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Full de As par les 3");
        }
    }
}
