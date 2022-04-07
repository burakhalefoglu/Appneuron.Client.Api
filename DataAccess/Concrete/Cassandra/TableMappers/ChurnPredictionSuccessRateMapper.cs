using Cassandra.Mapping;
using Core.DataAccess.Cassandra.Configurations;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.Cassandra.TableMappers;

public class ChurnPredictionSuccessRateMapper : Mappings
{
    public ChurnPredictionSuccessRateMapper()
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        var cassandraConnectionSettings =
            configuration.GetSection("CassandraConnectionSettings").Get<CassandraConnectionSettings>();
        For<ChurnPredictionSuccessRateModel>()
            .TableName("churn_prediction_success_rates")
            .KeyspaceName(cassandraConnectionSettings.Keyspace)
            .PartitionKey("project_id")
            .ClusteringKey(new Tuple<string, SortOrder>("created_at", SortOrder.Descending))
            .Column(u => u.Id, cm => cm.WithName("id").WithDbType(typeof(long)))
            .Column(u => u.ProjectId, cm => cm.WithName("project_id").WithDbType(typeof(long)))
            .Column(u => u.Value, cm => cm.WithName("value").WithDbType(typeof(float)))
            .Column(u => u.Status, cm => cm.WithName("status").WithDbType(typeof(bool)))
            .Column(u => u.CreatedAt, cm => cm.WithName("created_at").WithDbType(typeof(DateTimeOffset)));
    }
}