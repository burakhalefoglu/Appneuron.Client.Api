using Core.Entities;

namespace Entities.Concrete.ChartModels
{
    public class LevelBaseSessionWithDifficulty : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public int LevelIndex { get; set; }
        public int DifficultyLevel { get; set; }
        public int SessionCount { get; set; }
        public float SessionTime { get; set; }
    }
}