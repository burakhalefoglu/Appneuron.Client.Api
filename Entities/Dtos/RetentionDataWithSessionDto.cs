using System;
using System.Collections.Generic;

namespace Entities.Dtos
{
    public class RetentionDataWithSessionDto
    {
        public DateTime MinSession { get; set; }
        public DateTime MaxSession { get; set; }
        public List<RetentionDataDto> RetentionDataDtoList { get; set; }
    }
}