using Microsoft.AspNetCore.Mvc;
using PongStatTracker.Models;

namespace PongStatTracker.Services{
    public interface IQueryService
    {

        public Task<List<BrotherNames>> GetBrotherNames();

        public Task<int> AddGameStats(string player, int sinks, int hits, int misses, string opponent1, string opponent2, string winYN);

        public Task<List<BrotherGames>> GetBrotherGames(string player);

        public Task<List<HighestAverageStat>> GetHighestAvgStats(string stat);
        public Task<List<HighestTotalStat>> GetHighestTotalStats(string stat);
        public Task<List<HighestWinPercentage>> GetHighestWinPercentage();
        public Task<List<HighestGamesPlayed>> GetHighestGamesPlayed();
        public Task<List<HighestPlusMinus>> GetHighestPlusMinus();

    }
}

