using Core.Entities;

namespace Entities.Concrete
{
    public class EveryLoginLevelData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public string Levelname { get; set; }
        public int LevelsDifficultylevel { get; set; }
        public int PlayingTime { get; set; }
        public int AverageScores { get; set; }
        public int IsDead { get; set; }
        public int TotalPowerUsage { get; set; }
    }
}