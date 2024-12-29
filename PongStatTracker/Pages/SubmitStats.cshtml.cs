using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PongStatTracker.Models;
using PongStatTracker.Services;

namespace PongStatTracker.Pages;

public class SubmitStatsModel : PageModel
{
    private IQueryService _service;

    public SubmitStatsModel(IQueryService service){
        _service = service;
    }

    public bool? posted {get; set;}

    [BindProperty]
    public List<BrotherNames> SearchResult {get; set;}

    [BindProperty]
    public string? Player {get; set;}

    [BindProperty]
    public string? WinYN {get; set;}
    [BindProperty]
    public string? SinkNum {get; set;}
    [BindProperty]
    public string? HitNum {get; set;}
    [BindProperty]
    public string? MissNum {get; set;}

    [BindProperty]
    public string? Opponent1 {get; set;}
    [BindProperty]
    public string? Opponent2 {get; set;}

    public string? holder {get; set;}


    public async Task<ActionResult> OnGet()
    {
        try{
            SearchResult = await _service.GetBrotherNames();
            posted = false;
            return Page();
        }catch(Exception ex){
            return Page();
        }
        
    }

    public async Task<ActionResult> OnPost(){

        try{
            if (!ModelState.IsValid)
            {
                SearchResult = await _service.GetBrotherNames();
                return Page();
            }

            SearchResult = await _service.GetBrotherNames();
            var validNames = SearchResult.Select(b => b.Name).ToList();


            // Validate Inputs are correctly formatted and not null
            if (!validNames.Contains(Player) || Player.IsNullOrEmpty())
            {
                ModelState.AddModelError("Player", "*Please select a valid Player name from the list*");
                return Page();
            }
            else if (!validNames.Contains(Opponent1) || Opponent1.IsNullOrEmpty())
            {
                ModelState.AddModelError("Opponent1", "*Please select a valid Opponent 1 from the list*");
                return Page();
            }
            else if (!validNames.Contains(Opponent2) || Opponent2.IsNullOrEmpty())
            {
                ModelState.AddModelError("Opponent2", "*Please select a valid Opponent 2 from the list*");
                return Page();
            }
            else if (Player == Opponent1 || Player == Opponent2 || Opponent1 == Opponent2)
            {
                ModelState.AddModelError("Player", "*Each player can only be selected once*");
                return Page();
            }
            else if(!int.TryParse(SinkNum, out _) || SinkNum.IsNullOrEmpty())
            {
                ModelState.AddModelError("SinkNum", "*Please enter a valid number of Sinks*");
                return Page();
            } 
            else if(!int.TryParse(HitNum, out _) || HitNum.IsNullOrEmpty())
            {
                ModelState.AddModelError("SinkNum", "*Please enter a valid number of Hits*");
                return Page();
            } 
            else if(!int.TryParse(MissNum, out _) || MissNum.IsNullOrEmpty())
            {
                ModelState.AddModelError("SinkNum", "*Please enter a valid number of Misses*");
                return Page();
            }
            else if (string.IsNullOrEmpty(WinYN))
            {
                ModelState.AddModelError("WinYN", "*Please select either Win or Loss*");
                return Page();
            }

            await _service.AddGameStats(Player, int.Parse(SinkNum), int.Parse(HitNum), int.Parse(MissNum), Opponent1, Opponent2, WinYN);
            
            ModelState.Clear();

            // Player = null;
            // HitNum = null;
            // SinkNum = null;
            // MissNum = null;
            // Opponent1 = null;
            // Opponent2 = null;
            // WinYN = null;

            posted = true;

            return Page();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Player", "An Error Occurred... Please try again" );
                SearchResult = await _service.GetBrotherNames();
                return Page();
            }

    }
}
