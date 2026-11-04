namespace Blackjack;

internal sealed class Player
{
    public string Name { get; private set; }
    public int Balance { get; private set; }
    public Hand Hand { get; private set; }
    public int RoundsPlayed { get; private set; } = 0;

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

    public bool Bet(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (Balance < amount)
        {
            return false;
        }

        Balance -= amount;
        return true;
    }

    public void FinishRound(int amount)
    {
        Balance += amount;
        RoundsPlayed++;
    }
}
