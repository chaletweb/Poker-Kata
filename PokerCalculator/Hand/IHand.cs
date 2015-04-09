using System;
using System.Collections.Generic;

namespace PokerCalculator.Hand
{
    public interface IHand : IComparable<IHand>, IEquatable<IHand>
    {
        HandType HandType { get; }
        List<Card> SelectedCards { get; }
    }
}