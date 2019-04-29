using System;
using System.Threading.Tasks;
using Dapper;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.IQueries;

namespace ControlBot.DAL.Queries
{
    internal class CaseConcretyDateQuery : BaseQuery<CaseDate>, IQuery<CaseDate, CaseDateId>
    {
        public CaseConcretyDateQuery(ISession session) : base(session)
        {
        }

        //----------------------------------------------------------------//

        public Task<CaseDate> GetItemAsync(CaseDateId id)
        {
            String caseQuery = $"SELECT * FROM {TableName} WHERE Case_Id = @{nameof(id.Case_Id)} AND Specific_date = @{nameof(id.Specific_Date)}";
            return Connection.QueryFirstOrDefaultAsync<CaseDate>(caseQuery, id, Transaction);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> IsExistAsync(CaseDate item)
        {
            String isExistQuery = $"SELECT COUNT(*)::integer FROM {TableName} WHERE Case_Id = @{nameof(item.Id.Case_Id)} AND Specific_date = @{nameof(item.Id.Specific_Date)}";
            return await Connection.ExecuteScalarAsync<Int32>(isExistQuery, item.Id, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

    }
}
