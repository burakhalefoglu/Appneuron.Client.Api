using Cassandra.Mapping;
using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.TableMappers;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra;

public class CassChurnPredictionMlResultRepository: CassandraRepositoryBase<ChurnPredictionMlResultModel>, 
    IChurnPredictionMlResultRepository
    {
        public CassChurnPredictionMlResultRepository() 
            : base(MappingConfiguration.Global.Define<ChurnPredictionMlResultMapper>())
        {
        }
    }