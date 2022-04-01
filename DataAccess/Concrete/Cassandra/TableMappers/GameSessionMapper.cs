using Cassandra.Mapping;
using Core.DataAccess.Cassandra.Configurations;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.Cassandra.TableMappers;

public class GameSessionMapper: Mappings
{
    public GameSessionMapper()
    {
        var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        var cassandraConnectionSettings =
            configuration.GetSection("CassandraConnectionSettings").Get<CassandraConnectionSettings>();
        For<GameSessionModel>()
            .TableName("game_sessions")
            .KeyspaceName(cassandraConnectionSettings.Keyspace)
            .PartitionKey("project_id")
            .ClusteringKey(new Tuple<string, SortOrder>("created_at", SortOrder.Descending))
            .Column(u => u.Id, cm => cm.WithName("id").WithDbType(typeof(long)))
            .Column(u => u.ProjectId, cm => cm.WithName("project_id").WithDbType(typeof(long)))
            .Column(u => u.ClientId, cm => cm.WithName("client_id").WithDbType(typeof(long)))
            .Column(u => u.SessionStartTime, cm => cm.WithName("session_start_time").WithDbType(typeof(DateTimeOffset)))
            .Column(u => u.SessionFinishTime, cm => cm.WithName("session_finish_time").WithDbType(typeof(DateTimeOffset)))
            .Column(u => u.SessionTime, cm => cm.WithName("session_time").WithDbType(typeof(float)))
            .Column(u => u.Status, cm => cm.WithName("status").WithDbType(typeof(bool)));
    }
}