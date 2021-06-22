
using System;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.ChartModels;

namespace DataAccess.Abstract
{
    public interface IPlayerCountOnDifficultyLevelRepository : IDocumentDbRepository<PlayerCountOnDifficultyLevel>
    {
    }
}