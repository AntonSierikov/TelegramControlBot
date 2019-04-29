using System;
using System.Collections.Generic;
using ControlBot.Core.Enums;
using ControlBot.Core.Constants;

namespace ControlBot.BL.Messages
{
    public class CaseMessages
    {

        //----------------------------------------------------------------//

        public const String SELECT_SCHEDULE_TYPE = "Please, select schedule type for your case";

        public const String SELECT_TIME = "Please, select time for your case";

        public const String CASE_ALREADY_EXISTS = "Case already exists";

        public const String CASE_DAILY = "Daily";

        public const String CASE_WEEKLY = "Weekly";

        public const String CASE_DAY = "Specific days";

        //----------------------------------------------------------------//

        public static String CaseAdded(ScheduleType type, String name, String time)
        {
            return $"{type} case with name {name} and time {time} was added";
        }

        //----------------------------------------------------------------//

        public static String CaseNotAdded(ScheduleType type, String name, String time)
        {
            return $"{type} case with name {name} and time {time} wasn't added";
        }

        //----------------------------------------------------------------//
            
        public static String CaseNotExist(String nameCase) => $"Case with name {nameCase} isn't exist";

        //----------------------------------------------------------------//

        public static String DayOfWeekNotParsed(IEnumerable<String> enumerable)
        {
            String s_days = String.Join(StringConstants.COMA_SPACE, enumerable);
            return $"Can't parse {s_days}";
        }

        //----------------------------------------------------------------//
    }
}
