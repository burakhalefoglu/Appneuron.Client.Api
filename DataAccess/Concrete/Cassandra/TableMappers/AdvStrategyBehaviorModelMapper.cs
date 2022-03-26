using Cassandra.Mapping;
using Core.DataAccess.Cassandra.Configurations;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.Cassandra.TableMappers;

public class AdvStrategyBehaviorModelMapper : Mappings
{
    public AdvStrategyBehaviorModelMapper()
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        var cassandraConnectionSettings =
            configuration.GetSection("CassandraConnectionSettings").Get<CassandraConnectionSettings>();
        For<AdvStrategyBehaviorModel>()
            .TableName("logs")
            .KeyspaceName(cassandraConnectionSettings.Keyspace)
            .PartitionKey("id")
            .ClusteringKey(new Tuple<string, SortOrder>("time_stamp", SortOrder.Descending))
            .Column(u => u.Id, cm => cm.WithName("id").WithDbType(typeof(long)))
            .Column(u => u.Status, cm => cm.WithName("status").WithDbType(typeof(bool)));
    }
}