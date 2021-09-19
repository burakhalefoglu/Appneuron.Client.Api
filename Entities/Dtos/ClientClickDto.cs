using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text; 

namespace Entities.Dtos
{
    public class ClientClickDto: IDto
    {
        public string ClientId { get; set; }
        public int ClickCount { get; set; }
    }
}
