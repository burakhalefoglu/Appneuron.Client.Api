using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{ 
    public class LevelbaseSessionDto: IDto
    {
        public string ClientId { get; set; }
        public string LevelName { get; set; }
        public DateTime SessionStartTime { get; set; }

    }
}
