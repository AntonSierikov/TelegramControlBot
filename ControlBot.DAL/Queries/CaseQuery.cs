using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using ControlBot.Core.Entities;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.IQueries;

namespace ControlBot.DAL.Queries
{
    internal class CaseQuery : BaseQuery<Case>, ICaseQuery
    {

        //----------------------------------------------------------------//

        public CaseQuery(ISession session) : base(session)
        {
        }

        //----------------------------------------------------------------//

        public Task<Case> GetItemAsync(Int32 id)
        {
            String getItem = $"SELECT * FROM {TableName} WHERE Id = @{nameof(id)} LIMIT 1";
            return Connection.QueryFirstOrDefaultAsync<Case>(getItem, new { id }, Transaction);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> IsExistAsync(Case item)
        {
            String isExist = $"SELECT COUNT(Id)::integer FROM {TableName} WHERE casename = @{nameof(item.CaseName)}";
            return await Connection.ExecuteScalarAsync<Int32>(isExist, new { item.CaseName }, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//
        
        public Task<Case> GetCaseByNameAsync(String caseName)
        {
            String getCase = $"SELECT * FROM {TableName} WHERE casename = @{nameof(caseName)}";
            return Connection.QueryFirstOrDefaultAsync<Case>(getCase, new { caseName }, Transaction);
        }

        //----------------------------------------------------------------//

        public Task<IEnumerable<Case>> GetDailyActiveCasesAsync()
        {
            String getCases = $@"SELECT c.*, u.* FROM {TableName} c 
                                 LEFT JOIN case_day_of_week w ON c.id = w.case_id AND w.day_of_week = (SELECT EXTRACT(isodow from now()::date))
                                 LEFT JOIN case_specific_date d ON d.case_id = c.id AND d.specific_date = now()::date
                                 LEFT JOIN history h ON h.caseid = c.id
                                 JOIN control_user u ON nextuserid = u.id 
                                 WHERE nextuserId IS NOT NULL AND (h.issuccess IS NULL OR h.issuccess = '0')";
            return Connection.QueryAsync<Case, ControlUser, Case>(getCases, (c, u) =>
            {
                c.NextUser = u;
                return c;
            }, null, Transaction, splitOn: "id");
        }

        //----------------------------------------------------------------//

    }
}
