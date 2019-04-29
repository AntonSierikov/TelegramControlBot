using System;
using System.Collections.Generic;
using ControlBot.Core.Enums; 

namespace ControlBot.Core.Entities
{
    public class ConcretyDateCase : Case
    {

        //----------------------------------------------------------------//

        public List<CaseDate> Specific_Dates { get; set; }

        //----------------------------------------------------------------//

        public ConcretyDateCase(String nameCase, TimeSpan timeSpan, Int64 chatId)
            : base(nameCase, timeSpan, chatId)
        {
            Specific_Dates = new List<CaseDate>();
            ScheduleType = ScheduleType.ConcretyDate;
        }

        //----------------------------------------------------------------//

    }
}
