﻿
using System;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IOfferBehaviorModelRepository : IDocumentDbRepository<OfferBehaviorModel>
    {
    }
}