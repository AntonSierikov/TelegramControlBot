using System;
using System.Collections.Generic;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;

namespace ControlBot.DAL.Commands
{
    internal class CaseDayOfWeekCommand : BaseCommand<CaseDayOfWeek, CaseDayOfWeekId>, ICommand<CaseDayOfWeek, CaseDayOfWeekId>
    {

        //----------------------------------------------------------------//

        public CaseDayOfWeekCommand(ISession session) : base(session)
        {
        }
        
        //----------------------------------------------------------------//

        protected override string DeleteQuery(CaseDayOfWeek entity)
        {
            return $"DELETE FROM {TableName} WHERE Id = @{nameof(entity.Id.Case_Id)} AND @{nameof(entity.Id.Day_Of_Week)}";
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> InsertStatementQuery(CaseDayOfWeek entity)
        {
            String insert = $@"INSERT INTO {TableName} VALUES
                            (@{nameof(entity.Id.Case_Id)}, @{nameof(entity.Id.Day_Of_Week)})
                            RETURNING CaseId, DayOfWeek";
            return new KeyValuePair<String, Object>(insert, entity.Id);
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> UpdateStatementQuery(CaseDayOfWeek entity)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------------------------//

    }
}
