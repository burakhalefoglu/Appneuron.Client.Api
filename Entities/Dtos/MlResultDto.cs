using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class MlResultDto : IDto
    {
        public int CenterOfDifficultyLevel { get; set; }
        public int RangeCount { get; set; }

    }
}