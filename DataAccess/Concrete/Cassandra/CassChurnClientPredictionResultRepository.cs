using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra
{
    public class CassChurnClientPredictionResultRepository : CassandraRepositoryBase<ChurnPredictionMlResultModel>,
        IChurnClientPredictionResultRepository
    {
        public CassChurnClientPredictionResultRepository(CassandraContextBase cassandraContexts, string tableQuery) :
            base(
                cassandraContexts.CassandraConnectionSettings, tableQuery)
        {
        }
    }
}