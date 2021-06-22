using System;
using System.Linq;
using Core.DataAccess;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Core.DataAccess.MongoDb.Concrete;
using Entities.Concrete.ChartModels;

namespace DataAccess.Concrete.MongoDb
{
    public class StatisticsByNumberRepository : MongoDbRepositoryBase<StatisticsByNumber>, IStatisticsByNumberRepository
    {
        public StatisticsByNumberRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}