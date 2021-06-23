﻿using Core.DataAccess.MongoDb.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete;

namespace DataAccess.Concrete.MongoDb
{
    public class AdvEventRepository : MongoDbRepositoryBase<AdvEvent>, IAdvEventRepository
    {
        public AdvEventRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}