using Core.DataAccess.MongoDb.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete.ChartModels;

namespace DataAccess.Concrete.MongoDb
{
    public class LevelBasePlayerCountWithDifficultyRepository : MongoDbRepositoryBase<LevelBasePlayerCountWithDifficulty>, ILevelBasePlayerCountWithDifficultyRepository
    {
        public LevelBasePlayerCountWithDifficultyRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}