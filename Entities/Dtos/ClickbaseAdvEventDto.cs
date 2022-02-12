using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class ClickbaseAdvEventDto : IDto
    {
        public ClientClickDto[] ClientClickDtoList { get; set; }
        public DateTime TrigerdDay { get; set; }
    }
}