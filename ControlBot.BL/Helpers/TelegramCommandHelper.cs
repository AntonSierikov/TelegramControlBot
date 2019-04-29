using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ControlBot.BL.Helpers
{
    public class TelegramCommandHelper
    {
        public const String commandPattern = @"^\/(\w*)*";

        //----------------------------------------------------------------//

        public static Boolean TryParse(String text, out String command)
        {
            command = null;
            Regex regex = new Regex(commandPattern);
            Match match = regex.Match(text);

            if (match.Success)
            {
                command = match.Groups[0].Value;
                return true;
            } 
            else
            {
                return false;
            }
        }

        //----------------------------------------------------------------//

    }
}
