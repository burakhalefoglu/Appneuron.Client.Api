
using System;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IEveryLoginLevelDataRepository : IDocumentDbRepository<EveryLoginLevelData>
    {
    }
}