using Cassandra.Mapping;
using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.TableMappers;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra;

public class CassAdvStrategyBehaviorModelRepository: CassandraRepositoryBase<AdvStrategyBehaviorModel>, 
    IAdvStrategyBehaviorModelRepository
{
    public CassAdvStrategyBehaviorModelRepository() : 
        base(MappingConfiguration.Global.Define<AdvStrategyBehaviorModelMapper>())
    {
    }
}