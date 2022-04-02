using Cassandra.Mapping;
using Core.DataAccess.Cassandra.Configurations;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.Cassandra.TableMappers;

public class OfferBehaviorMapper : Mappings
{
    public OfferBehaviorMapper()
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        var cassandraConnectionSettings =
            configuration.GetSection("CassandraConnectionSettings").Get<CassandraConnectionSettings>();
        For<OfferBehaviorModel>()
            .TableName("offer_behaviors")
            .KeyspaceName(cassandraConnectionSettings.Keyspace)
            .PartitionKey("project_id", "offer_id")
            .ClusteringKey(new Tuple<string, SortOrder>("created_at", SortOrder.Descending))
            .Column(u => u.Id, cm => cm.WithName("id").WithDbType(typeof(long)))
            .Column(u => u.ProjectId, cm => cm.WithName("project_id").WithDbType(typeof(long)))
            .Column(u => u.ClientId, cm => cm.WithName("client_id").WithDbType(typeof(long)))
            .Column(u => u.Version, cm => cm.WithName("version").WithDbType(typeof(short)))
            .Column(u => u.OfferId, cm => cm.WithName("offer_id").WithDbType(typeof(int)))
            .Column(u => u.IsBuyOffer, cm => cm.WithName("is_buy_offer").WithDbType(typeof(byte)))
            .Column(u => u.Status, cm => cm.WithName("status").WithDbType(typeof(bool)))
            .Column(u => u.CreatedAt, cm => cm.WithName("created_at").WithDbType(typeof(DateTimeOffset)))
            ;
    }
}