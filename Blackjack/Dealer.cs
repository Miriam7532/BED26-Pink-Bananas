namespace Blackjack;

internal sealed class Dealer
{
    public Hand Hand { get; private set; }

    public Dealer()
    {
        Hand = new Hand();
    }

    // Adds one card to the dealer's hand
    public void Draw(Card card)
    {
        Hand.AddCard(card);
    }
}