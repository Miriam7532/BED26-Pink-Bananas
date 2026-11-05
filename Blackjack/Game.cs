using System;
using Blackjack;

namespace Blackjack.Game;

internal sealed class Game
{
    private readonly Player _player;
    private readonly Dealer _dealer;
    private readonly Deck _deck;

    public Game(Player player, Deck deck)
    {
        _player = player;
        _dealer = new Dealer();
        _deck = deck;
    }

    // Starts a new Blackjack round
    public void PlayRound(int bet)
    {
        // Reset hands and prepare the deck
        _player.Hand.ResetHand();
        _dealer.Hand.ResetHand();
        _deck.PrepareForNewRound();

        Console.WriteLine("\n=== New Blackjack Round ===");

        DealInitialCards();

        // Check for Blackjack before normal turns
        if (CheckBlackjack(bet))
        {
            return;
        }

        PlayerTurn();

        // Player loses immediately if their points exceed 21
        if (_player.Hand.IsBust())
        {
            FinishRound(0, "You went over 21.", bet);
            return;
        }

        DealerTurn();

        DetermineWinner(bet);
    }

    // Deals two cards to the player and dealer
    private void DealInitialCards()
    {
        _player.Draw(_deck.DrawCard());
        _dealer.Draw(_deck.DrawCard());

        _player.Draw(_deck.DrawCard());
        _dealer.Draw(_deck.DrawCard());

        // Show only the dealer's first card
        Console.WriteLine(
            $"\nDealer: {_dealer.Hand.Cards[0]} [?]"
        );

        PrintHand("You", _player.Hand);
    }

    // Checks whether the player or dealer has Blackjack
    private bool CheckBlackjack(int bet)
    {
        bool playerBlackjack = _player.Hand.IsBlackjack();
        bool dealerBlackjack = _dealer.Hand.IsBlackjack();

        if (!playerBlackjack && !dealerBlackjack)
        {
            return false;
        }

        RevealDealer();

        // Both have Blackjack: return the bet
        if (playerBlackjack && dealerBlackjack)
        {
            FinishRound(
                bet,
                "Both have Blackjack. It's a tie!",
                bet
            );
        }
        // Player has Blackjack: return bet + 1.5x profit
        else if (playerBlackjack)
        {
            int blackjackProfit = bet * 3 / 2;

            FinishRound(
                bet + blackjackProfit,
                "Blackjack! You win!",
                bet
            );
        }
        // Dealer has Blackjack: player loses their bet
        else
        {
            FinishRound(
                0,
                "Dealer has Blackjack.",
                bet
            );
        }

        return true;
    }

    // Handles the player's turn
    private void PlayerTurn()
    {
        while (_player.Hand.Points < 21)
        {
            Console.Write("Hit (h) or Stand (s)? ");

            string? choice = Console.ReadLine()?.Trim().ToLower();

            if (choice == "s")
            {
                break;
            }

            if (choice == "h")
            {
                Card card = _deck.DrawCard();

                _player.Draw(card);

                Console.WriteLine($"You drew: {card}");

                PrintHand("You", _player.Hand);
            }
            else
            {
                Console.WriteLine(
                    "Invalid choice. Please enter h or s."
                );
            }
        }
    }

    // Dealer draws cards until reaching at least 17
    private void DealerTurn()
    {
        RevealDealer();

        while (_dealer.Hand.Points < 17)
        {
            Card card = _deck.DrawCard();

            _dealer.Draw(card);

            Console.WriteLine($"Dealer drew: {card}");

            PrintHand("Dealer", _dealer.Hand);
        }

        if (_dealer.Hand.IsBust())
        {
            Console.WriteLine("Dealer went over 21.");
        }
        else
        {
            Console.WriteLine("Dealer stands.");
        }
    }

    // Determines the winner of the round
    private void DetermineWinner(int bet)
    {
        int playerPoints = _player.Hand.Points;
        int dealerPoints = _dealer.Hand.Points;

        // Dealer busts: player wins
        if (_dealer.Hand.IsBust())
        {
            FinishRound(
                bet * 2,
                "You win!",
                bet
            );
        }
        // Player has more points
        else if (playerPoints > dealerPoints)
        {
            FinishRound(
                bet * 2,
                "You win!",
                bet
            );
        }
        // Dealer has more points
        else if (playerPoints < dealerPoints)
        {
            FinishRound(
                0,
                "Dealer wins.",
                bet
            );
        }
        // Equal points: return the bet
        else
        {
            FinishRound(
                bet,
                "It's a tie!",
                bet
            );
        }
    }

    // Reveals the dealer's complete hand
    private void RevealDealer()
    {
        PrintHand("Dealer", _dealer.Hand);
    }

    // Displays a hand and its total points
    private static void PrintHand(string label, Hand hand)
    {
        Console.WriteLine(
            $"{label}: {string.Join(", ", hand.Cards)} = {hand.Points}"
        );
    }

    // Updates balance and displays the result
    private void FinishRound(int payout, string message, int bet)
    {
        // The bet has already been deducted by Player.Bet()
        _player.FinishRound(payout);

        // Calculate profit or loss relative to the original bet
        int profit = payout - bet;

        string details = profit > 0
            ? $"Profit: {profit} kr."
            : profit < 0
                ? $"Loss: {bet} kr."
                : "Bet returned.";

        Console.WriteLine(
            $"{message} {details} Balance: {_player.Balance} kr."
        );
    }
}