using Cassandra.Mapping;
using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.TableMappers;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra;

public class CassGameSessionRepository: CassandraRepositoryBase<GameSessionModel>, IGameSessionRepository
{
    public CassGameSessionRepository() : base(MappingConfiguration.Global.Define<GameSessionMapper>())
    {
    }
}