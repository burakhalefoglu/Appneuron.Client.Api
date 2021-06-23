using Core.Entities;

namespace Entities.Concrete.ChartModels
{
    public class ChallengeBasedSegmentation : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public long CompetitiveClientCount { get; set; }
        public long NonCompetitiveClientCount { get; set; }
    }
}