using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerCalculator
{
    public class Dealer : IDealer
    {
        public List<Card> CardPack { get; set; }

        public Dealer()
        {
            InitializeCardPack(null, null);
        }

        public void InitializeCardPack(List<Player> players, Board board )
        {
            CardPack = new List<Card>();
            PopulateAndMixCardPack();
            
            if (players != null && board != null)
            {
                GetBackAllCards(players, board);
            }
        }

        private void GetBackAllCards(List<Player> players, Board board)
        {
            players.ForEach(x => x.Cards.Clear());
            board.ClearCards();
        }

        public void PopulateAndMixCardPack()
        {
            var cards = new List<Card>();

            PopulateCardPack(cards, ColorCard.Trefle);
            PopulateCardPack(cards, ColorCard.Pique);
            PopulateCardPack(cards, ColorCard.Carreau);
            PopulateCardPack(cards, ColorCard.Coeur);

            CardPack.AddRange(cards.OrderBy(a => Guid.NewGuid()));
        }

        public void PopulateCardPack(List<Card> cards, ColorCard colorCard)
        {
            for (var i = 2; i <= 14; i++)
            {
                cards.Add(new Card(i, colorCard));
            }
        }

        public void GiveCardToPlayer(Player player, Card card)
        {
            RemoveCardFromPack(card);
            player.AddCard(card);
        }

        public void GiveCardToPlayer(Player player)
        {
            var card = GetCardFromPack();
            player.AddCard(card);
        }

        public void GiveCardToPlayer(Player player, int times)
        {
            for (int i = 0; i < times; i++)
            {
                GiveCardToPlayer(player);
            }
        }

        public Card GetCardFromPack()
        {
            var card = CardPack[0];
            CardPack.Remove(card);
            return card;
        }

        public void GiveCardToBoard(Board board, Card card)
        {
            RemoveCardFromPack(card);
            board.AddCard(card);
        }

        public void GiveCardToBoard(Board board)
        {
            Card card = GetCardFromPack();
            board.AddCard(card);
        }

        public void GiveCardToBoard(Board board, int times)
        {
            for (var i = 0; i < times; i++)
            {
                GiveCardToBoard(board);
            }
        }

        private void RemoveCardFromPack(Card card)
        {
            if (card.Value == 1)
                card.Value = 14;

            Card cardFromPack = CardPack.FirstOrDefault(x => x.Value == card.Value && x.Color == card.Color);
            if (cardFromPack == null)
                throw new Exception(string.Format("Card {0} {1} doesn't exist", card.Value, card.Color));

            CardPack.Remove(cardFromPack);
        }
    }
}