using PongStatTracker.Models;
using PongStatTracker.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace PongStatTracker.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<BrotherNames> Bnames {get; set;}
        public DbSet<BrotherGames> Bgames {get; set;}
        public DbSet<HighestAverageStat> AvgStat {get; set;}
        public DbSet<HighestTotalStat> TotalStat {get; set;}
        public DbSet<HighestWinPercentage> HighWinP {get; set;}
        public DbSet<HighestPlusMinus> HighPlusMinus {get; set;}
        public DbSet<HighestGamesPlayed> HighGP {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BrotherNames>().HasNoKey();
            modelBuilder.Entity<BrotherGames>().HasNoKey();
            modelBuilder.Entity<HighestAverageStat>().HasNoKey();
            modelBuilder.Entity<HighestTotalStat>().HasNoKey();
            modelBuilder.Entity<HighestWinPercentage>().HasNoKey();
            modelBuilder.Entity<HighestPlusMinus>().HasNoKey();
            modelBuilder.Entity<HighestGamesPlayed>().HasNoKey();
        }
    }
}