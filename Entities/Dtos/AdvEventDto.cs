using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class AdvEventDto : IDto
    {
        public string ClientId { get; set; }
        public string TrigersInlevelName { get; set; }
        public string AdvType { get; set; }
        public int DifficultyLevel { get; set; }
        public DateTime TrigerdTime { get; set; }
    }
}
