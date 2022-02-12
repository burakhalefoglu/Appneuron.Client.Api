using Core.Entities;

namespace Entities.Dtos
{
    public class LevelbaseFinishScoreDto : IDto
    {
        public long ClientId { get; set; }
        public string Levelname { get; set; }
        public int FinishScores { get; set; }
    }
}