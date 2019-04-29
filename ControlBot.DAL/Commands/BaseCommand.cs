using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ControlBot.DAL.Abstract;
using Dapper;

namespace ControlBot.DAL.Commands
{
    internal abstract class BaseCommand<T, TId> : BaseDbOperation<T>
    {
        public BaseCommand(ISession session)
            : base(session)
        {}

        //----------------------------------------------------------------//

        protected abstract KeyValuePair<String, Object> InsertStatementQuery(T entity);

        protected abstract KeyValuePair<String, Object> UpdateStatementQuery(T entity);

        protected abstract String DeleteQuery(T entity);

        //----------------------------------------------------------------//

        public TId Insert(T entity)
        {
            KeyValuePair<String, Object> insertStatement = InsertStatementQuery(entity);
            return Connection.QueryFirstOrDefault<TId>(insertStatement.Key, insertStatement.Value, Transaction);
        }

        //----------------------------------------------------------------//
        
        public Task<TId> InsertAsync(T entity)
        {
            KeyValuePair<String, Object> insertStatement = InsertStatementQuery(entity);
            return Connection.QueryFirstOrDefaultAsync<TId>(insertStatement.Key, insertStatement.Value, Transaction);
        }

        //----------------------------------------------------------------//

        public Boolean Update(T entity)
        {
            KeyValuePair<String, Object> updateStatement = UpdateStatementQuery(entity);
            return Connection.Execute(updateStatement.Key, updateStatement.Value, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> UpdateAsync(T entity)
        {
            KeyValuePair<String, Object> updateStatement = UpdateStatementQuery(entity);
            return await Connection.ExecuteAsync(updateStatement.Key, updateStatement.Value, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

        public Boolean Delete(T entity)
        {
            return Connection.Execute(DeleteQuery(entity), entity, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> DeleteAsync(T entity)
        {
            return await Connection.ExecuteAsync(DeleteQuery(entity), entity, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

    }
}
