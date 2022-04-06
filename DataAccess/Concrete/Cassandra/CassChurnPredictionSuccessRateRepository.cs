using Cassandra.Mapping;
using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.TableMappers;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra;

public class CassChurnPredictionSuccessRateRepository : CassandraRepositoryBase<ChurnPredictionSuccessRateModel>,
    IChurnPredictionSuccessRateRepository
{
    public CassChurnPredictionSuccessRateRepository()
        : base(MappingConfiguration.Global.Define<ChurnPredictionSuccessRateMapper>())
    {
    }
}