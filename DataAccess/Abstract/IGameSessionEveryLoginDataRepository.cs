using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IGameSessionEveryLoginDataRepository : IDocumentDbRepository<GameSessionEveryLoginData>
    {
    }
}