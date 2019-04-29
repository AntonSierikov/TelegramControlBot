using System;
using System.Collections.Generic;
using System.Text;

namespace ControlBot.Core.Constants
{
    public class TelegramCommandConstants
    {
        public const String REGISTER = "/register";

        public const String REGISTER_USER = "/register_user";

        public const String CREATE_DAILY_CASE = "/create_daily_case";

        public const String CREATE_WEEKLY_CASE = "/create_weekly_case";

        public const String CREATE_SPECIFIC_DATE_CASE = "/create_case_for_specific_date";

        public const String Update_Sequence = "/update_sequence";

        public const String CHANGE_CASE = "/change_case";

        public const String WHOS_NEXT = "/who's next?";

        public const String BAD_USER = "/bad_user";

        public const String BAD_USER_FOR_ALL = "/bad_user_for_all_cases";

        public const String MAKE_CASE = @"^\+$";
    }
}
