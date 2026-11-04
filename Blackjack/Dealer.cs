using System;

namespace Blackjack;

internal sealed class Dealer
{
    public Hand Hand { get; private set; }

    public Dealer()
    {
        Hand = new Hand();
    }
}
