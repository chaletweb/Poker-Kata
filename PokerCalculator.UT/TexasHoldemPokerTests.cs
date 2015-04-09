using NFluent;
using NUnit.Framework;
using PokerCalculator.Engine;

namespace PokerCalculator.UT
{
    public class TexasHoldemPokerTests
    {
        private PokerTable _pokerTable;
        private IDealer _dealer;

        private Player _player1;
        private Player _player2;

        [SetUp]
        public void SetUp()
        {
            _dealer = new Dealer();
            _pokerTable = new PokerTable(_dealer, new TexasHoldemEngine());

            _player1 = new Player("Player1");
            _pokerTable.AddPlayer(_player1);
            _player2 = new Player("Player2");
            _pokerTable.AddPlayer(_player2);
        }
        [Test]
        public void ShouldReturnHauteur3WhenPlayerHasA3()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(3));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Hauteur 3");
        }

        [Test]
        public void ShouldReturnHauteurRoiWhenPlayerHasARoi()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(13));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Hauteur Roi");
        }

        [Test]
        public void ShouldReturnHauteur8WhenPlayerHasA8And7()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8));
            _dealer.GiveCardToPlayer(_player1, new Card(7));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Hauteur 8");
        }

        [Test]
        public void ShouldReturnPaire8WhenPlayerHasA8And8()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Carreau));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Paire de 8");
        }

        [Test]
        public void ShouldReturnPaire8WhenPlayerHasA8And8With5Card()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(11, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(CardValue.As, ColorCard.Pique));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Paire de 8");
        }

        [Test]
        public void ShouldReturn2Paire8And7WhenPlayerHasA8And7()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Pique));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Coeur));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Paire de 8 et de 7");
        }

        [Test]
        public void ShouldReturn2Paire8And7WhenPlayerHasA8And7And2()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(2, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(2, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(8, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(8, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Pique));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Paire de 8 et de 7");
        }

        [Test]
        public void ShouldReturnBrelan4WhenPlayerHas44468()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Carreau));
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(4, ColorCard.Pique));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(6, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(8, ColorCard.Pique));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Brelan de 4");
        }

        [Test]
        public void ShouldReturnFull4WhenPlayerHasA44466()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Pique));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(4, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(6, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(6, ColorCard.Coeur));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Full de 4 par les 6");
        }

        [Test]
        public void ShouldReturnCarre4WhenPlayerHasA44446()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(4, ColorCard.Carreau));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(4, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(6, ColorCard.Pique));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Carre de 4");
        }

        [Test]
        public void ShouldReturnColorTrefleWhenPlayerHas5Trefles()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(11, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(8, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(12, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(6, ColorCard.Trefle));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Couleur Trefle");
        }

        [Test]
        public void ShouldReturnQuinteWhenPlayerHas5SuiteCard()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(4, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(5, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(6, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(7, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(8, ColorCard.Trefle));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Quinte au 8");
        }

        [Test]
        public void ShouldReturnQuinteWhenPlayerHas5SuiteCardWith1()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(14, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(2, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(4, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(5, ColorCard.Trefle));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Quinte au 5");
        }

        [Test]
        public void ShouldReturnQuinteWhenPlayerHas5SuiteCardWithAs()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(11, ColorCard.Coeur));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(12, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(13, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(14, ColorCard.Trefle));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Quinte au As");
        }

        [Test]
        public void ShouldReturnQuinteFlushWhenPlayerHas5SuiteCardWith1()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(14, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(2, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(3, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(4, ColorCard.Trefle));
            _dealer.GiveCardToBoard(_pokerTable.Board, new Card(5, ColorCard.Trefle));
            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Quinte Flush au 5");
        }

        [Test]
        public void ShouldReturnPlayer2WhenPlayerWin()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(8, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(7, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player2, new Card(7, ColorCard.Coeur));

            Check.That(_pokerTable.HandOfPlayer(_player1).ToString()).Equals("Hauteur 8");
            Check.That(_pokerTable.HandOfPlayer(_player2).ToString()).Equals("Hauteur 9");

            Check.That(_pokerTable.WhoWin()[0].Name).Equals("Player2");
        }

        [Test]
        public void ShouldReturnPlayer2WhenPlayerWinWithAPaire()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(7, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Coeur));

            Check.That(_pokerTable.WhoWin()[0].Name).Equals("Player2");
        }

        [Test]
        public void ShouldReturnPlayer2WhenPlayerWinWithAUpperPaire()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player2, new Card(9, ColorCard.Coeur));

            Check.That(_pokerTable.WhoWin()[0].Name).Equals("Player1");
        }

        [Test]
        public void ShouldReturnPlayer2WhenPlayerWinWithBestHand()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(CardValue.As, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(CardValue.Roi, ColorCard.Pique));
            _dealer.GiveCardToPlayer(_player2, new Card(CardValue.Roi, ColorCard.Coeur));

            Check.That(_pokerTable.WhoWin()[0].Name).Equals("Player2");
        }

        [Test]
        public void ShouldReturnEqualWhenPlayersWinWithAUpperPaire()
        {
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Trefle));
            _dealer.GiveCardToPlayer(_player1, new Card(10, ColorCard.Pique));

            _dealer.GiveCardToPlayer(_player2, new Card(10, ColorCard.Carreau));
            _dealer.GiveCardToPlayer(_player2, new Card(10, ColorCard.Coeur));

            Check.That(_pokerTable.WhoWin().Count).Equals(2);
        }
    }
}
