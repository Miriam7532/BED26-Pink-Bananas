
using System;
using System.Collections.Generic;

namespace Blackjack;

public class Deck
{
    private List<Card> cards = new List<Card>();
    private Random random = new Random();

    // Returns the number of cards remaining
    public int CardsRemaining
    {
        get { return cards.Count; }
    }

    // Constructor: creates and shuffles a new deck
    public Deck()
    {
        CreateDeck();
        
    }

    // Creates all 52 cards
    private void CreateDeck()
    {
        cards.Clear();

        string[] suits =
        {
            "Hearts", "Diamonds", "Clubs", "Spades"
        };

        string[] ranks =
        {
            "Ace", "2", "3", "4", "5", "6", "7",
            "8", "9", "10", "Jack", "Queen", "King"
        };

        foreach (string suit in suits)
        {
            foreach (string rank in ranks)
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    // Randomly shuffles the deck
   
    // Draws one card from the deck
    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("The deck is empty.");
        }

        Card drawnCard = cards[0];
        cards.RemoveAt(0);

        return drawnCard;
    }

    // Checks whether a new deck is needed
    public void PrepareForNewRound()
    {
        if (cards.Count < 15)
        {
            CreateDeck();
            
        }
    }
}
