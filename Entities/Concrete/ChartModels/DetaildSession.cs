using Core.Entities;
using Entities.Concrete.ChartModels.OneToOne;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete.ChartModels
{
    public class DetaildSession : DocumentDbEntity
    {
        public string ProjectId { get; set; }
        public AvarageSessionByLevel[] AvarageSessionByLevel { get; set; }
    }
}
