using System;
using System.Linq;
using Core.DataAccess;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb.Context;
using Core.DataAccess.MongoDb.Concrete;
namespace DataAccess.Concrete.MongoDb
{
    public class GameSessionEveryLoginDataRepository : MongoDbRepositoryBase<GameSessionEveryLoginData>, IGameSessionEveryLoginDataRepository
    {
        public GameSessionEveryLoginDataRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}