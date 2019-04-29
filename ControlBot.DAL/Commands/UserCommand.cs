using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Entities;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;

namespace ControlBot.DAL.Commands
{
    internal class UserCommand : BaseCommand<ControlUser, Int32>, ICommand<ControlUser, Int32>
    {
        public UserCommand(ISession session) 
            : base(session)
        {}

        //----------------------------------------------------------------//

        protected override String DeleteQuery(ControlUser entity) => $"DELETE FROM {TableName} WHERE Id = @{nameof(entity.Id)}";

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> InsertStatementQuery(ControlUser entity)
        {
            String insert = $@"INSERT INTO {TableName} VALUES(@{nameof(entity.Id)},  @{nameof(entity.UserName)}, @{nameof(entity.FirstName)}, @{nameof(entity.LastName)})
                               RETURNING Id";
            return new KeyValuePair<String, Object>(insert, entity);
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> UpdateStatementQuery(ControlUser entity)
        {
            String update = $@"UPDATE {TableName} SET
                               username = @{nameof(entity.UserName)}
                               firstname = @{nameof(entity.FirstName)},
                               lastname = @{nameof(entity.LastName)}
                               WHERE Id = @{nameof(entity.Id)}";
            return new KeyValuePair<String, Object>(update, entity);
        }

        //----------------------------------------------------------------//

    }
}
