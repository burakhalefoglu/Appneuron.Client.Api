using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class RetentionDataDto : IDto
    {
        public DateTime Day { get; set; }
        public ChurnClientDto[] ClientDtoList { get; set; }
    }
}