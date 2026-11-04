using System;

namespace Blackjack;

internal sealed class Player
{
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public Hand Hand { get; private set; }

    public Player(string name, decimal balance = 100)
    {
        Name = name;
        Balance = balance;
        Hand = new Hand();
    }
}
