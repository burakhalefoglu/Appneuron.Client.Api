using Core.DataAccess;
using Entities.Concrete.ChartModels;

namespace DataAccess.Abstract
{
    public interface ISuccessAttemptRateWithDifficultyRepository : IDocumentDbRepository<SuccessAttemptRateWithDifficulty>
    {
    }
}