using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LevelbaseFinishScoreDto : IDto 
    {
        public long ClientId { get; set; }
        public string Levelname { get; set; }
        public int FinishScores { get; set; }

    }
}
