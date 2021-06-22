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
    public class ProjectBaseDailySessionRepository : MongoDbRepositoryBase<ProjectDailySession>, IProjectBaseDailySessionRepository
    {
        public ProjectBaseDailySessionRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}