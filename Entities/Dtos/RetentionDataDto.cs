using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class RetentionDataDto : IDto
    {
        public DateTime Day { get; set; }
        public ClientDto[] clientDtoList { get; set; }

    }
}
