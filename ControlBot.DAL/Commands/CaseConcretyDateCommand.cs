using System;
using System.Collections.Generic;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;

namespace ControlBot.DAL.Commands
{
    internal class CaseConcretyDateCommand : BaseCommand<CaseDate, CaseDateId>, ICommand<CaseDate, CaseDateId>
    {

        //----------------------------------------------------------------//

        public CaseConcretyDateCommand(ISession session) : base(session)
        {}

        //----------------------------------------------------------------//

        protected override string DeleteQuery(CaseDate entity)
        {
           return $"DELETE FROM {TableName} WHERE Case_Id = @{nameof(entity.Id.Case_Id)} AND Specific_Date = @{nameof(entity.Id.Specific_Date)}";
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> InsertStatementQuery(CaseDate entity)
        {
            String insertQuery = $@"INSERT INTO {TableName} VALUES
                                  (@{nameof(entity.Id.Case_Id)}, @{nameof(entity.Id.Specific_Date)})
                                  RETURNING Case_Id, Specific_Date";
            return new KeyValuePair<String, Object>(insertQuery, entity);
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> UpdateStatementQuery(CaseDate entity)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------------------------//

    }
}
