using PokerCalculator.Engine;

namespace PokerCalculator.Console
{
    class Program
    {
        static void Main()
        {
            Dealer dealer = new Dealer();
            PokerTable table = new PokerTable(dealer, new TexasHoldemEngine());

            Player me = new Player("Eric");
            table.AddPlayer(me);

            for (int i = 0; i < 5; i++)
            {
                dealer.GiveCardToPlayer(me);
                dealer.GiveCardToPlayer(me);

                dealer.GiveCardToBoard(table.Board, 5);

                System.Console.WriteLine("PLAYER");
                foreach (var card in me.Cards)
                {
                    System.Console.WriteLine("{0} {1}", card, card.Color);
                }

                System.Console.WriteLine("BOARD");
                foreach (var card in table.Board.Cards)
                {
                    System.Console.WriteLine("{0} {1}", card, card.Color);
                }


                System.Console.WriteLine("RESULTAT");
                var hand = table.HandOfPlayer(me);
                System.Console.WriteLine("Main ====> {0}", hand);
                
                foreach (var card in hand.SelectedCards)
                {
                    System.Console.WriteLine("{0} {1}", card, card.Color);
                }

                System.Console.WriteLine("----------------------------------");

                dealer.InitializeCardPack(table.Players, table.Board);
            }

            System.Console.ReadKey();
        }
    }
}
