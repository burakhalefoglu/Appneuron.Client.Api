using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels.OneToOne
{
    public class SuccessAttemptWithDifficulty
    {
        public int DifficultyLevel { get; set; }
        public long SuccessAttempt { get; set; }
    }
}
