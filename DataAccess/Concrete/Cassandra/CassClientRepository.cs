using Cassandra.Mapping;
using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.TableMappers;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra;

public class CassClientRepository : CassandraRepositoryBase<ClientDataModel>,
    IClientRepository
{
    public CassClientRepository() : base(MappingConfiguration.Global.Define<ClientMapper>())
    {
    }
}