using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Abstract;
using ControlBot.Core.Indentifiers;

namespace ControlBot.Core.Entities
{
    public class CaseDayOfWeek : DbObject<CaseDayOfWeekId>
    {

        //----------------------------------------------------------------//

        public Case Case { get; set; }

        public DayOfWeek Day_Of_Week { get; set; }

        //----------------------------------------------------------------//

        public CaseDayOfWeek(Int32 caseId, DayOfWeek dayOfWeek)
        {
            Id.Case_Id = caseId;
            Id.Day_Of_Week = (Int32)dayOfWeek;
        }

        //----------------------------------------------------------------//

        public CaseDayOfWeek(Case @case, DayOfWeek dayOfWeek)
            : this(@case.Id, dayOfWeek)
        {
            Case = @case;
        }

        //----------------------------------------------------------------//

    }
}
