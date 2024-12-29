using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PongStatTracker.Services;
using PongStatTracker.Models;

namespace PongStatTracker.Pages;

public class BrothersModel : PageModel
{
    private readonly IQueryService _service;
    
    public List<BrotherNames> BrotherList { get; set; }
    public List<BrotherGames> BrotherGameList { get; set; }
    
    [BindProperty]
    public string SelectedBrother { get; set; }

    public bool ClearSearch { get; set; }

    public int TotalGames {get; set;}
    public int GamesWon {get; set;}
    public int GamesLost {get; set;}
    public int Ranking {get; set;}
    public double WinPercent {get; set;}
    public int TotalSinks {get; set;}
    public int TotalHits {get; set;}
    public int TotalMisses {get; set;}
    public double HitsPerGame {get; set;}
    public double SinksPerGame {get; set;}
    public double MissesPerGame {get; set;}
    public double GamePlusMinus {get; set;}

    public BrothersModel(IQueryService service)
    {
        _service = service;
    }

    public async Task<ActionResult> OnGetAsync()
    {
        BrotherList = await _service.GetBrotherNames();
        ClearSearch = true;
        return Page();
    }

    public async Task<ActionResult> OnPostAsync()
    {
        // Get the brother list again to maintain the search functionality
        BrotherList = await _service.GetBrotherNames();
        
        if (!string.IsNullOrEmpty(SelectedBrother))
        {
            ClearSearch = false;
            BrotherGameList = await _service.GetBrotherGames(SelectedBrother);
            TotalGames = BrotherGameList.Count;
            
            if(BrotherGameList.Count > 0){
            foreach(var game in BrotherGameList){
                if(game.WinYN == "Y"){GamesWon += 1;}
                else{GamesLost += 1;}

                TotalHits += game.Hits;
                TotalSinks += game.Sinks;
                TotalMisses += game.Misses;
            }

            WinPercent = ((double)GamesWon/TotalGames)*100;
            SinksPerGame = ((double)TotalSinks/TotalGames);
            HitsPerGame = ((double)TotalHits/TotalGames);
            MissesPerGame = ((double)TotalMisses/TotalGames);
            GamePlusMinus = HitsPerGame + SinksPerGame - MissesPerGame;
            }else{
                GamesWon = 0;
                GamesLost = 0;
                TotalHits = 0;
                TotalSinks = 0;
                WinPercent = 0;
                SinksPerGame = 0;
                HitsPerGame = 0;
                TotalMisses = 0;
                MissesPerGame = 0;
                GamePlusMinus = 0;
            }

            return Page();
        }

        return Page();
    }
}
