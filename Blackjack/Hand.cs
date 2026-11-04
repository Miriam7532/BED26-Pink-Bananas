using System.Drawing;

namespace Blackjack;

internal sealed class Hand
{
    private readonly List<CardPlaceholder> _cards;
    public IReadOnlyList<CardPlaceholder> Cards => _cards;

    public int Points { get; private set; } = 0;

    public Hand()
    {
        _cards = [];
    }

    public void AddCard(CardPlaceholder card)
    {
        _cards.Add(card);
    }

    public void CalculatePoints()
    {
        int tempPoints = 0;

        foreach (var card in _cards)
        {
            if (card.IsAce && tempPoints > 21)
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

    public void ResetHand()
    {
        Points = 0;
        _cards.Clear();
    }
}

internal sealed class CardPlaceholder
{
    public int Value { get; set; }
    public bool IsAce { get; set; }
}