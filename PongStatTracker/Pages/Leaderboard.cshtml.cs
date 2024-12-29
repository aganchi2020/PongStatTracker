using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using PongStatTracker.Models;
using PongStatTracker.Services;

namespace PongStatTracker.Pages;

public class LeaderboardModel : PageModel
{
    private IQueryService _service;

    public LeaderboardModel(IQueryService service){
        _service = service;
    }
    public List<PlayerStats> TopPlayers { get; set; }
    public List<PlayerStats> TopSinks { get; set; }
    public List<PlayerStats> TopWinPercentage { get; set; }
    public List<PlayerStats> TopAccuracy { get; set; }
    public List<HighestAverageStat> HighestAvgSinks {get; set;}
    public List<HighestAverageStat> HighestAvgHits {get; set;}
    public List<HighestAverageStat> HighestAvgMisses {get; set;}
    public List<HighestTotalStat> HighestTotalSinks {get; set;}
    public List<HighestTotalStat> HighestTotalHits {get; set;}
    public List<HighestTotalStat> HighestTotalMisses {get; set;}
    public List<HighestGamesPlayed> HighestGP {get; set;}
    public List<HighestPlusMinus> HighestPM {get; set;}
    public List<HighestWinPercentage> HighestWP {get; set;}

    public async Task<IActionResult> OnGet()
    {
        // Initialize with mock data for now
        TopPlayers = GenerateMockData();
        TopSinks = GenerateMockData().Take(5).ToList();
        TopWinPercentage = GenerateMockData().Take(5).ToList();
        TopAccuracy = GenerateMockData().Take(5).ToList();

        HighestAvgSinks = await _service.GetHighestAvgStats("Sinks");
        HighestAvgHits = await _service.GetHighestAvgStats("Hits");
        HighestAvgMisses = await _service.GetHighestAvgStats("Misses");

        HighestTotalSinks = await _service.GetHighestTotalStats("Sinks");
        HighestTotalHits = await _service.GetHighestTotalStats("Hits");
        HighestTotalMisses = await _service.GetHighestTotalStats("Misses");

        HighestGP = await _service.GetHighestGamesPlayed();
        HighestPM = await _service.GetHighestPlusMinus();
        HighestWP = await _service.GetHighestWinPercentage();

        return Page();
    }

    private List<PlayerStats> GenerateMockData()
    {
        return new List<PlayerStats>
        {
            new PlayerStats { Rank = 1, Name = "Placeholder 1", Games = 45, WinPercentage = 75.5, SinksPerGame = 8.2, PointDifferential = 124 },
            new PlayerStats { Rank = 2, Name = "Placeholder 2", Games = 38, WinPercentage = 68.4, SinksPerGame = 7.8, PointDifferential = 98 },
            new PlayerStats { Rank = 3, Name = "Placeholder 3", Games = 42, WinPercentage = 61.9, SinksPerGame = 7.1, PointDifferential = 67 },
            new PlayerStats { Rank = 4, Name = "Placeholder 4", Games = 35, WinPercentage = 54.3, SinksPerGame = 6.9, PointDifferential = 23 },
            new PlayerStats { Rank = 5, Name = "Placeholder 5", Games = 40, WinPercentage = 47.5, SinksPerGame = 6.5, PointDifferential = -12 },
        };
    }
}

public class PlayerStats
{
    public int Rank { get; set; }
    public string Name { get; set; }
    public int Games { get; set; }
    public double WinPercentage { get; set; }
    public double SinksPerGame { get; set; }
    public int PointDifferential { get; set; }
    public int Sinks { get; set; }
    public double Accuracy { get; set; }
}
