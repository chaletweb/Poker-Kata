using System;

namespace PokerCalculator
{
    public class Card : IEquatable<Card>, IComparable<Card>
    {
        public Card(int cardValue)
        {
            Value = cardValue;
        }

        public Card(int cardValue, ColorCard color)
        {
            Value = cardValue;
            Color = color;
        }

        public Card(CardValue figure, ColorCard color)
            : this((int)figure, color)
        {

        }

        public int Value { get; set; }
        public ColorCard Color { get; private set; }

        public bool Equals(Card other)
        {
            return Value == other.Value && Color == other.Color;
        }

        public int CompareTo(Card other)
        {
            if (Value > other.Value)
            {
                return 1;
            }
            if (Value < other.Value)
            {
                return -1;
            }
            return 0;
        }

        public override string ToString()
        {
            if (Value >= 1 && Value <= 10)
            {
                return Value.ToString();
            }
            if (Value == 11)
            {
                return "Valet";
            }
            if (Value == 12)
            {
                return "Dame";
            }
            if (Value == 13)
            {
                return "Roi";
            }
            if (Value == 14)
            {
                return "As";
            }

            return string.Empty;
        }
    }

    public enum CardValue
    {
        Valet = 11,
        Dame = 12,
        Roi = 13,
        As = 14
    }
}