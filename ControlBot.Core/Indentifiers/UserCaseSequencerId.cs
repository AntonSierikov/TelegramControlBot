using System;

namespace ControlBot.Core.Indentifiers
{
    public class UserCaseSequencerId
    {

        //----------------------------------------------------------------//

        public Int32 UserId { get; set; }

        public Int32 CaseId { get; set; }


        //----------------------------------------------------------------//

        public UserCaseSequencerId(Int32 userId, Int32 caseId)
        {
            UserId = userId;
            CaseId = caseId;
        }
    }
}
