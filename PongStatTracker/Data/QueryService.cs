using System;
using PongStatTracker.Models;
using PongStatTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;

namespace PongStatTracker.Data{
    
    public class QueryService : IQueryService
    {
        private readonly AppDbContext _appDbContext;

        public QueryService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<BrotherNames>> GetBrotherNames(){
            var results = await _appDbContext.Bnames.FromSql($"exec GetBrotherNames").ToListAsync();
            return results;
        }

        public async Task<int> AddGameStats(string player, int sinks, int hits, int misses, string opponent1, string opponent2, string winYN)
        {
            var parameters = new[]
            {
                new SqlParameter("@PlayerName", player),
                new SqlParameter("@Sinks", sinks),
                new SqlParameter("@Hits", hits),
                new SqlParameter("@Misses", misses),
                new SqlParameter("@Opponent1", opponent1),
                new SqlParameter("@Opponent2", opponent2),
                new SqlParameter("@WinYN", winYN)
            };

            // Execute the stored procedure
            return await _appDbContext.Database
                .ExecuteSqlRawAsync("EXEC CreateGameEntry @PlayerName, @Sinks, @Hits, @Misses, @Opponent1, @Opponent2, @WinYN", parameters);
        }

        public async Task<List<BrotherGames>> GetBrotherGames(string player){
            var results = await _appDbContext.Bgames.FromSql($"exec GetPlayerGameEntries {player}").ToListAsync();
            return results;
        }

         public async Task<List<HighestAverageStat>> GetHighestAvgStats(string stat){
            var results = await _appDbContext.AvgStat.FromSql($"exec GetHighestAvgStats {stat}").ToListAsync();
            return results;
         }

         public async Task<List<HighestTotalStat>> GetHighestTotalStats(string stat){
            var results = await _appDbContext.TotalStat.FromSql($"exec GetHighestTotalStats {stat}").ToListAsync();
            return results;
         }
         
        public async Task<List<HighestWinPercentage>> GetHighestWinPercentage(){ 
            var results = await _appDbContext.HighWinP.FromSql($"exec GetHighestWinPercentage").ToListAsync();
            return results;
        }

        public async Task<List<HighestGamesPlayed>> GetHighestGamesPlayed(){
            var results = await _appDbContext.HighGP.FromSql($"exec GetHighestGamesPlayed").ToListAsync();
            return results;
        }

        public async Task<List<HighestPlusMinus>> GetHighestPlusMinus(){
            var results = await _appDbContext.HighPlusMinus.FromSql($"exec GetHighestPlusMinus").ToListAsync();
            return results;
        }
    }
}