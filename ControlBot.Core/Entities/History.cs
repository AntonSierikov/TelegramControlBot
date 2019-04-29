using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Abstract;

namespace ControlBot.Core.Entities
{
    public class History : DbObject<Int32>
    {
        public DateTime RemindTime { get; private set; }

        public Boolean IsSuccess { get; private set; }

        public Int32 UserId { get; private set; }

        public Int32 CaseId { get; private set; }

        public ControlUser User { get; private set; }

        public Case Case { get; private set; }

        //----------------------------------------------------------------//

        public History(DateTime remindTime, Int32 userId, Int32 caseId)
        {
            RemindTime = remindTime;
            UserId = userId;
            CaseId = caseId;
        }

        //----------------------------------------------------------------//

        public History(DateTime remindTime, ControlUser user, Case @case)
            : this(remindTime, user.Id, @case.Id)
        {
            User = user;
            Case = @case;
        }

        //----------------------------------------------------------------//

    }
}
