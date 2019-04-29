using System;
using System.Collections.Generic;
using ControlBot.Core.Enums;

namespace ControlBot.Core.Entities
{
    public class WeeklyCase : Case
    {

        //----------------------------------------------------------------//

        public List<DayOfWeek> DayOfWeeks { get; set; }

        //----------------------------------------------------------------//

        public WeeklyCase(String nameCase, TimeSpan timeSpan, Int64 chatId, List<DayOfWeek> dayOfWeeks)
            : base(nameCase, timeSpan, chatId)
        {
            DayOfWeeks = dayOfWeeks;
            ScheduleType = ScheduleType.Weekly;
        }

        //----------------------------------------------------------------//
            
    }
}
