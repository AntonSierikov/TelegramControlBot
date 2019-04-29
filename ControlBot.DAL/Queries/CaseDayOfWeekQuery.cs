using System;
using System.Threading.Tasks;
using Dapper;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.IQueries;

namespace ControlBot.DAL.Queries
{
    internal class CaseDayOfWeekQuery : BaseQuery<CaseDayOfWeek>, IQuery<CaseDayOfWeek, CaseDayOfWeekId>
    {

        //----------------------------------------------------------------//

        public CaseDayOfWeekQuery(ISession session) : base(session)
        {
        }

        //----------------------------------------------------------------//

        public Task<CaseDayOfWeek> GetItemAsync(CaseDayOfWeekId id)
        {
            String getCaseDate = $"SELECT * FROM {TableName} WHERE Case_Id = @{nameof(id.Case_Id)} AND Day_Of_Week = @{nameof(id.Day_Of_Week)}";
            return Session.Connection.QueryFirstOrDefaultAsync<CaseDayOfWeek>(getCaseDate, id, Transaction);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> IsExistAsync(CaseDayOfWeek item)
        {
            String isExist = $"SELECT COUNT(*)::integer FROM {TableName} WHERE Case_Id = @{nameof(item.Id.Case_Id)} AND Day_Of_Week = @{nameof(item.Id.Day_Of_Week)}";
            return await Connection.ExecuteScalarAsync<Int32>(isExist, item.Id, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//


    }
}
