using Core.DataAccess.Cassandra;
using DataAccess.Abstract;
using DataAccess.Concrete.Cassandra.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.Cassandra
{
    public class CassGameSessionEveryLoginDataRepository : CassandraRepositoryBase<GameSessionEveryLoginData>,
        IGameSessionEveryLoginDataRepository
    {
        public CassGameSessionEveryLoginDataRepository(CassandraContextBase cassandraContexts, string tableQuery) :
            base(
                cassandraContexts.CassandraConnectionSettings, tableQuery)
        {
        }
    }
}