using Core.DataAccess.MongoDb.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete.ChartModels;

namespace DataAccess.Concrete.MongoDb
{
    public class SuccessAttemptRateWithDifficultyRepository : MongoDbRepositoryBase<SuccessAttemptRateWithDifficulty>, ISuccessAttemptRateWithDifficultyRepository
    {
        public SuccessAttemptRateWithDifficultyRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}