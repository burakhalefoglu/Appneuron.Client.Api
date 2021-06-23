using Core.Entities;
using System;

namespace Entities.Concrete.ChartModels
{
    public class PlayerCountsOnLevel : DocumentDbEntity
    {
        public string ProjectID { get; set; }
        public long TotalPlayerCount { get; set; }
        public int LevelIndex { get; set; }
        public DateTime DateTime { get; set; }
        public long PaidPlayerCount { get; set; }
    }
}