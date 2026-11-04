namespace Blackjack;

internal sealed class Hand
{
    private readonly List<Card> _cards;
    public IReadOnlyList<Card> Cards => _cards;

    public int Points { get; private set; } = 0;

    public Hand()
    {
        _cards = [];
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
        CalculatePoints();
    }

    private void CalculatePoints()
    {
        int tempPoints = 0;

        foreach (var card in _cards)
        {
            if (card.Rank == "Ace" && tempPoints + card.Value > 21)
            {
                tempPoints += 1;
                continue;
            }

            tempPoints += card.Value;
        }

        Points = tempPoints;
    }

    public bool IsBlackjack()
    {
        if (_cards.Count == 2 && Points == 21)
        {
            return true;
        }

        return false;
    }

    public bool IsBust()
    {
        if (Points > 21)
        {
            return true;
        }

        return false;
    }

    public void ResetHand()
    {
        Points = 0;
        _cards.Clear();
    }
}