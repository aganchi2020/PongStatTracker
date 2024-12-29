namespace PongStatTracker.Models{

    public class BrotherGames{
        
        public int GameID {get; set;}
        public string PlayerName {get; set;}
        public int Sinks {get; set;}
        public int Hits {get; set;}
        public int Misses {get; set;}
        public string Opponent1 {get; set;}
        public string Opponent2 {get; set;}
        public string WinYN {get; set;}
        public DateTime GameDate {get; set;}

    }
}