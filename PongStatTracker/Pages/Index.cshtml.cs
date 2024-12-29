using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PongStatTracker.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public List<PlayerStats> TopPlayers { get; set; }
    public List<PlayerStats> TopSinks { get; set; }
    public List<PlayerStats> TopWinPercentage { get; set; }
    public List<PlayerStats> TopAccuracy { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        // Initialize with mock data for now
        TopPlayers = GenerateMockData();
        TopSinks = GenerateMockData().Take(5).ToList();
        TopWinPercentage = GenerateMockData().Take(5).ToList();
        TopAccuracy = GenerateMockData().Take(5).ToList();

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


