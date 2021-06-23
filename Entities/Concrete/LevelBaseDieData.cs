using Core.Entities;

namespace Entities.Concrete
{
    public class LevelBaseDieData : DocumentDbEntity
    {
        public string ClientId { get; set; }
        public string ProjectID { get; set; }
        public string CustomerID { get; set; }
        public int DiyingTimeAfterLevelStarting { get; set; }
        public string levelName { get; set; }
        public int DiyingDifficultyLevel { get; set; }
        public float DiyingLocationX { get; set; }
        public float DiyingLocationY { get; set; }
        public float DiyingLocationZ { get; set; }
    }
}