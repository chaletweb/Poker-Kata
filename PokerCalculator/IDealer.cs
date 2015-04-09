using System.Collections.Generic;

namespace PokerCalculator
{
    public interface IDealer
    {
        List<Card> CardPack { get; set; }
        void InitializeCardPack(List<Player> players, Board board );
        void PopulateAndMixCardPack();
        void PopulateCardPack(List<Card> cards, ColorCard colorCard);
        void GiveCardToPlayer(Player player, Card card);
        void GiveCardToPlayer(Player player);
        void GiveCardToPlayer(Player player, int times);
        Card GetCardFromPack();
        void GiveCardToBoard(Board board, Card card);
        void GiveCardToBoard(Board board);
        void GiveCardToBoard(Board board, int times);
    }
}