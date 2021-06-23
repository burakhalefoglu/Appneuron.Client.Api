using Core.DataAccess;
using Entities.Concrete.ChartModels;

namespace DataAccess.Abstract
{
    public interface ILevelBasePowerUsageWithDifficultyRepository : IDocumentDbRepository<LevelBasePowerUsageWithDifficulty>
    {
    }
}