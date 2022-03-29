using Cassandra.Mapping;
using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.TableMappers;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra;

public class CassOfferBehaviorModelRepository : CassandraRepositoryBase<OfferBehaviorModel>,
    IOfferBehaviorModelRepository
{
    public CassOfferBehaviorModelRepository() : base(MappingConfiguration.Global.Define<LogMapper>())
    {
    }
}