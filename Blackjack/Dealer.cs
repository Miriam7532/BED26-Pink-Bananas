using System;

namespace Blackjack;

internal sealed class Dealer
{
    private const int maxPoints = 17;
    public Hand Hand { get; private set; }

    public Dealer()
    {
        Hand = new Hand();
    }

    public void Draw(Card card)
    {
        while (Hand.Points <= maxPoints)
        {
            Hand.AddCard(card);
        }
    }
}
