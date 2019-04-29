using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.IQueries;

namespace ControlBot.DAL.Queries
{
    internal class UserCaseSequencerQuery : BaseQuery<UserCaseSequencer>, IQuery<UserCaseSequencer, UserCaseSequencerId>

    {

        //----------------------------------------------------------------//

        public UserCaseSequencerQuery(ISession session) : base(session)
        {}

        //----------------------------------------------------------------//

        public Task<UserCaseSequencer> GetItemAsync(UserCaseSequencerId id)
        {
            String sequencerQuery = $"SELECT * FROM {TableName} WHERE userid = @{nameof(id.UserId)} AND caseid = @{nameof(id.CaseId)}";
            return Connection.QueryFirstOrDefaultAsync<UserCaseSequencer>(sequencerQuery, id, Transaction);
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> IsExistAsync(UserCaseSequencer item)
        {
            String isExistQuery = $@"SELECT COUNT(*)::integer FROM {TableName} 
                                     WHERE userid = @{nameof(item.Id.UserId)} AND caseid = @{nameof(item.Id.CaseId)}";
            return await Connection.ExecuteScalarAsync<Int32>(isExistQuery, item.Id, Transaction) > default(Int32);
        }

        //----------------------------------------------------------------//

    }
}
