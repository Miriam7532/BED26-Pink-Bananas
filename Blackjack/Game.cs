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
    if (!_player.Bet(bet))
    {
        Console.WriteLine("Invalid bet.");
        return;
    }

    _deck.PrepareForNewRound();

    _player.Hand.ResetHand();
    _dealer.Hand.ResetHand();

    Console.WriteLine("\n=== New Blackjack Round ===");

    DealInitialCards();

    if (CheckBlackjack(bet))
    {
        return;
    }

    PlayerTurn();

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

        Console.WriteLine(
            $"\nDealer: {_dealer.Hand.Cards[0]} [?]"
        );

        PrintHand("You", _player.Hand);
    }

    // Checks for Blackjack
    private bool CheckBlackjack(int bet)
    {
        bool playerBlackjack = _player.Hand.IsBlackjack();
        bool dealerBlackjack = _dealer.Hand.IsBlackjack();

        if (!playerBlackjack && !dealerBlackjack)
        {
            return false;
        }

        RevealDealer();

        if (playerBlackjack && dealerBlackjack)
        {
            FinishRound(
                0,
                "Both have Blackjack. It's a tie!",
                bet
            );
        }
        else if (playerBlackjack)
        {
            int blackjackProfit = bet * 3 / 2;

            FinishRound(
                blackjackProfit,
                "Blackjack! You win!",
                bet
            );
        }
        else
        {
            FinishRound(
                -bet,
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

    // Dealer draws until reaching at least 17
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

    // Determines the winner
    private void DetermineWinner(int bet)
    {
        int playerPoints = _player.Hand.Points;
        int dealerPoints = _dealer.Hand.Points;

        if (_dealer.Hand.IsBust())
        {
            FinishRound(bet, "You win!", bet);
        }
        else if (playerPoints > dealerPoints)
        {
            FinishRound(bet, "You win!", bet);
        }
        else if (playerPoints < dealerPoints)
        {
            FinishRound(-bet, "Dealer wins.", bet);
        }
        else
        {
            FinishRound(0, "It's a tie!", bet);
        }
    }

    // Reveals the dealer's cards
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

    // Updates the player's balance after a round
    private void FinishRound(int change, string message, int bet)
    {
        _player.FinishRound(change);

        string details = change > 0
            ? $"Profit: {change} kr."
            : change < 0
                ? $"Loss: {bet} kr."
                : "Bet returned.";

        Console.WriteLine(
            $"{message} {details} Balance: {_player.Balance} kr."
        );
    }
}