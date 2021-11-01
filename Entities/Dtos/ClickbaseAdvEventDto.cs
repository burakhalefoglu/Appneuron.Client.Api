using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ClickbaseAdvEventDto : IDto
    {
        public ClientClickDto[] ClientClickDtoList { get; set; }
        public DateTime TrigerdDay { get; set; }

    }
} 
