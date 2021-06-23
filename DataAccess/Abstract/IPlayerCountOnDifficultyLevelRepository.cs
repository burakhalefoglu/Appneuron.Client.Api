using Core.DataAccess;
using Entities.Concrete.ChartModels;

namespace DataAccess.Abstract
{
    public interface IPlayerCountOnDifficultyLevelRepository : IDocumentDbRepository<PlayerCountWithDifficulty>
    {
    }
}