﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BuyingEventDto : IDto
    {
        public string ClientId { get; set; }
        public DateTime TrigerdTime { get; set; }

    }
}