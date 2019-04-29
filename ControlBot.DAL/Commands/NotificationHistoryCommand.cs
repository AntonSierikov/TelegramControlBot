using ControlBot.Core.Entities;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace ControlBot.DAL.Commands
{
    internal class NotificationHistoryCommand : BaseCommand<History, Int32>, IHistoryCommand
    {
        public NotificationHistoryCommand(ISession session) : base(session)
        {
        }

        //----------------------------------------------------------------//

        public async Task<Boolean> SetHistoryAsSuccess(int historyId)
        {
            String update = $"UPDATE {TableName} SET issuccess = '1' WHERE id = @{nameof(historyId)}";
            return await Connection.ExecuteAsync(update, new { historyId }, Transaction) > 0;
        }

        //----------------------------------------------------------------//

        protected override String DeleteQuery(History entity)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> InsertStatementQuery(History entity)
        {
            String insertHistory = $@"INSERT INTO {TableName} 
                                      VALUES(DEFAULT, @{nameof(entity.IsSuccess)}, @{nameof(entity.UserId)}, @{nameof(entity.CaseId)})
                                      RETURING Id";
            return new KeyValuePair<String, Object>(insertHistory, entity);
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> UpdateStatementQuery(History entity)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------------------------//

    }
}
