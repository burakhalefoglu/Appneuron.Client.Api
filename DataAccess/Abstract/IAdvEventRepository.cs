﻿using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAdvEventRepository : IDocumentDbRepository<AdvEvent>
    {
    }
}