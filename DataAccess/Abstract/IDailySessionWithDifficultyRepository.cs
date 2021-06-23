using Core.DataAccess;
using Entities.Concrete.ChartModels;

namespace DataAccess.Abstract
{
    public interface IDailySessionWithDifficultyRepository : IDocumentDbRepository<DailySessionWithDifficulty>
    {
    }
}