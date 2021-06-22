using Core.Entities;
using Entities.Concrete.ChartModels.OneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class SuccessAttemptRate : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public SuccessAttemptWithDifficulty[] SuccessAttemptWithDifficulty { get; set; }
    }
}
