using System;

namespace Blackjack;

internal sealed class Player
{
    public string Name { get; private set; }
    public int Balance { get; private set; }
    public Hand Hand { get; private set; }

    public Player(string name, int balance = 100)
    {
        Name = name;
        Balance = balance;
        Hand = new Hand();
    }

    public void Draw(Card card)
    {
        Hand.AddCard(card);
    }
}
