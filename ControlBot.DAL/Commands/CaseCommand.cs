using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Entities;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;

namespace ControlBot.DAL.Commands
{
    internal class CaseCommand : BaseCommand<Case, Int32>, ICommand<Case, Int32>
    {

        //----------------------------------------------------------------//

        public CaseCommand(ISession session) : base(session)
        {
        }

        //----------------------------------------------------------------//

        protected override string DeleteQuery(Case entity)
        {
            return $"DELETE FROM {TableName} WHERE Id = @{nameof(entity.Id)}";
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> InsertStatementQuery(Case entity)
        {
            String insertQuery = $@"INSERT INTO {TableName} VALUES(
                                    DEFAULT, @{nameof(entity.CaseName)}, @{nameof(entity.ScheduleType)}, @{nameof(entity.TimeOfDay)},
                                    @{nameof(entity.NextUserId)}, @{nameof(entity.ChatId)}) RETURNING Id";
            return new KeyValuePair<String, Object>(insertQuery, entity);
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> UpdateStatementQuery(Case entity)
        {
            String updateQuery =  $@"UPDATE {TableName} SET
                                  CaseName = @{nameof(entity.CaseName)},
                                  TimeOfDay = @{nameof(entity.TimeOfDay)},
                                  ScheduleType = @{nameof(entity.ScheduleType)},
                                  NextUserId = @{nameof(entity.NextUserId)}
                                  WHERE Id = @{nameof(entity.Id)}";
            return new KeyValuePair<String, Object>(updateQuery, entity);
        }

        //----------------------------------------------------------------//

    }
}
