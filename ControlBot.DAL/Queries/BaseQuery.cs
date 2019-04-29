using System;
using System.Threading.Tasks;
using ControlBot.DAL.Abstract;
using Dapper;

namespace ControlBot.DAL.Queries
{
    internal abstract class BaseQuery<T> : BaseDbOperation<T>
    {

        //----------------------------------------------------------------//

        public BaseQuery(ISession session) : base(session)
        {}

        //----------------------------------------------------------------//

        public Task<Int32> CountAsync()
        {
            String count = $"SELECT Count(*)::integer FROM {TableName}";
            return Connection.QueryFirstOrDefaultAsync<Int32>(count, null, Transaction);
        }

        //----------------------------------------------------------------//

    }
}
