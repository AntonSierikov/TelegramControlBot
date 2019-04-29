using System;
using ControlBot.Core.Abstract;
using ControlBot.Core.Indentifiers;

namespace ControlBot.Core.Entities
{
    public class UserCaseSequencer : DbObject<UserCaseSequencerId>
    {
        public Int32 Sequence { get; set; }

        public ControlUser User { get; set; }

        public Case Case { get; set; }

        //----------------------------------------------------------------//

        public UserCaseSequencer(Int32 caseId, Int32 userId, Int32 sequence)
        {
            Id = new UserCaseSequencerId(userId, caseId);
            Sequence = sequence;
        }

        //----------------------------------------------------------------//

        public UserCaseSequencer(Case @case, ControlUser user, Int32 sequence)
            : this(@case.Id, user.Id, sequence)
        {
            User = user;
            Case = @case;
        }

        //----------------------------------------------------------------//


    }
}
