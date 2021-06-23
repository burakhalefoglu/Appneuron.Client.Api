using Core.Entities;

namespace Entities.Concrete.ChartModels
{
    public class PlayerCountWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int DifficultyLevel { get; set; }
        public long PlayerCount { get; set; }
    }
}