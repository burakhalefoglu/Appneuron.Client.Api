using Core.DataAccess.MongoDb.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete.ChartModels;

namespace DataAccess.Concrete.MongoDb
{
    public class PlayerListByDayRepository : MongoDbRepositoryBase<PlayerListByDayWithDifficulty>, IPlayerListByDayWithDifficultyRepository
    {
        public PlayerListByDayRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}