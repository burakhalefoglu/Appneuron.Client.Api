﻿using Core.Entities;

namespace Entities.Dtos;

public class OfferDto : IDto
{
    public string Name { get; set; }
    public string Version { get; set; }
    public long Id { get; set; }
}