﻿using Core.DataAccess.MongoDb.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete.ChartModels;

namespace DataAccess.Concrete.MongoDb
{
    public class BuyingCountWithDifficultyRepository : MongoDbRepositoryBase<BuyingCountWithDifficulty>, IBuyingCountWithDifficultyRepository
    {
        public BuyingCountWithDifficultyRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}