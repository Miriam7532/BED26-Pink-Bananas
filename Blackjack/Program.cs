using System;
using Blackjack;
using Blackjack.Game;

class Program
{
    static void Main(string[] args)
    {
       Leaderboard leaderboard = new Leaderboard();
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Blackjack ===");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Leaderboard");
            Console.WriteLine("3. Rules");
            Console.WriteLine("4. Exit");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayGameSession(leaderboard);
                    break;
                case "2":
                    leaderboard.Show();
                    break;
                case "3":
                    ShowRules();
                    break;
                case "4":
                    running = false;
                    Console.WriteLine("Thanks for playing!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4. Press any key.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void PlayGameSession(Leaderboard leaderboard)
{
    Console.Clear();

    Console.Write("Enter your name: ");
    string? name = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(name))
    {
        name = "Anonymous";
    }

    Player player = new Player(name);
    Deck deck = new Deck();
    Game game = new Game(player, deck);

    bool keepPlaying = true;

    while (keepPlaying && player.Balance > 0)
    {
        Console.WriteLine($"\nYour balance: {player.Balance} kr.");

        int bet;

        while (true)
        {
            Console.Write($"Enter your bet (1-{player.Balance}): ");

            if (int.TryParse(Console.ReadLine(), out bet)
                && bet >= 1
                && bet <= player.Balance)
            {
                break;
            }

            Console.WriteLine("Invalid bet. Please try again.");
        }

        game.PlayRound(bet);

        if (player.Balance == 0)
        {
            Console.WriteLine("You have no money left!");
            break;
        }

        Console.Write("\nPlay another round? (y/n): ");
        keepPlaying = Console.ReadLine()?.Trim().ToLower() == "y";
    }

    Console.WriteLine(
        $"\nGame over! {player.Name} finished with " +
        $"{player.Balance} kr after {player.RoundsPlayed} rounds."
    );

    leaderboard.AddEntry(
        player.Name,
        player.Balance,
        player.RoundsPlayed
    );

    Console.WriteLine("Result saved to the leaderboard!");
    Console.WriteLine("Press any key to return to the menu.");
    Console.ReadKey();
}

    static void ShowRules()
    {
        Console.Clear();
        Console.WriteLine("=== RULES ===");
        Console.WriteLine("- Get closer to 21 than the dealer without going over.");
        Console.WriteLine("- Aces are worth 11 or 1.");
        Console.WriteLine("- Jack, Queen, King are worth 10, other cards face value.");
        Console.WriteLine("- Dealer must draw cards until the sum is at least 17.");
        Console.WriteLine("\nPress any key to return to the menu.");
        Console.ReadKey();
    }
}