using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LevelbaseFailDto : IDto
    {
        public string ClientId { get; set; }
        public string levelName { get; set; }
        public int FailCount { get; set; }
    }
}
