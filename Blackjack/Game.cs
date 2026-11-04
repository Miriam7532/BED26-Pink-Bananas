namespace Blackjack.Game;

public class Game
{
    // Starts a new Blackjack round
    public void PlayRound()
    {
        Console.WriteLine("=== New Blackjack Round ===");

        DealInitialCards();
        PlayerTurn();
        DealerTurn();
        DetermineWinner();
    }

    // Deals two cards to the player and the dealer
    private void DealInitialCards()
    {
        Console.WriteLine("Dealing two cards to each player...");
    }

    // Handles the player's turn
    private void PlayerTurn()
    {
        Console.WriteLine("Player's turn...");
    }

    // Handles the dealer's turn
    private void DealerTurn()
    {
        Console.WriteLine("Dealer's turn...");
    }

    // Determines the winner of the round
    private void DetermineWinner()
    {
        Console.WriteLine("Determining the winner...");
    }
}