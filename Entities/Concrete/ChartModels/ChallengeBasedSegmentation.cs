using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class ChallengeBasedSegmentation : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public long CompetitiveClientCount { get; set; }
        public long NonCompetitiveClientCount { get; set; }
    }
}
