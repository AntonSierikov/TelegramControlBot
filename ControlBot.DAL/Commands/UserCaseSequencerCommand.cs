using System;
using System.Collections.Generic;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;

namespace ControlBot.DAL.Commands
{
    internal class UserCaseSequencerCommand : BaseCommand<UserCaseSequencer, UserCaseSequencerId>,
                                              ICommand<UserCaseSequencer, UserCaseSequencerId>
    {

        //----------------------------------------------------------------//

        public UserCaseSequencerCommand(ISession session) : base(session)
        {
        }

        //----------------------------------------------------------------//

        protected override string DeleteQuery(UserCaseSequencer entity)
        {
            throw new NotImplementedException();
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> InsertStatementQuery(UserCaseSequencer entity)
        {
            String insertSequencer = $@"INSERT INTO {TableName} VALUES(@{nameof(entity.Id.UserId)}, @{nameof(entity.Id.CaseId)}, @{nameof(entity.Sequence)})
                                        RETURNING userid, caseId";
            return new KeyValuePair<String, Object>(insertSequencer, new { entity.Id.CaseId, entity.Id.UserId, entity.Sequence });
        }

        //----------------------------------------------------------------//

        protected override KeyValuePair<String, Object> UpdateStatementQuery(UserCaseSequencer entity)
        {
            String updateSequencer = $@"UPDATE {TableName} SET
                                        Sequence = @{nameof(entity.Sequence)}
                                        WHERE UserId = @{nameof(entity.Id.UserId)} AND CaseId = @{nameof(entity.Id.CaseId)}";
            return new KeyValuePair<String, Object>(updateSequencer, new { entity.Id.CaseId, entity.Id.UserId, entity.Sequence });
        }

        //----------------------------------------------------------------//

    }
}
