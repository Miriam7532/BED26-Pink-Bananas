using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Leaderboard
{
    private const string FilePath = "leaderboard.json";
    private List<LeaderboardEntry> entries;

    public Leaderboard()
    {
        Load();
    }

    public void Load()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                entries = JsonSerializer.Deserialize<List<LeaderboardEntry>>(json) ?? new List<LeaderboardEntry>();
            }
            else
            {
                entries = new List<LeaderboardEntry>();
            }
        }
        catch (Exception)
        {
            
            entries = new List<LeaderboardEntry>();
        }
    }

    public void AddEntry(string name, int finalBalance, int roundsPlayed)
    {
        entries.Add(new LeaderboardEntry
        {
            Name = name,
            FinalBalance = finalBalance,
            RoundsPlayed = roundsPlayed
        });

        entries = entries.OrderByDescending(e => e.FinalBalance)
                         .Take(5)
                         .ToList();

        Save();
    }

    private void Save()
    {
        try
        {
            string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not save leaderboard: {ex.Message}");
        }
    }

    public void Show()
{
    Console.Clear();
    Console.WriteLine("=== LEADERBOARD ===");

    // Sort by highest balance and show only the top 5
    var topFive = entries
        .OrderByDescending(e => e.FinalBalance)
        .Take(5)
        .ToList();

    if (topFive.Count == 0)
    {
        Console.WriteLine("No results saved yet.");
    }
    else
    {
        int rank = 1;

        foreach (var entry in topFive)
        {
            Console.WriteLine(
                $"{rank}. {entry.Name,-10} " +
                $"{entry.FinalBalance,5} kr " +
                $"({entry.RoundsPlayed} rounds)"
            );

            rank++;
        }
    }

    Console.WriteLine("\nPress any key to return to the menu.");
    Console.ReadKey();
}
}