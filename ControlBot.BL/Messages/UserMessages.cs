using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Constants;

namespace ControlBot.BL.Messages
{
    public static class UserMessages
    {

        //----------------------------------------------------------------//

        public static String UsersNotExist(IEnumerable<String> names)
        {
            return $"User(s) aren't exist {String.Join(StringConstants.COMA_SPACE, names)}";
        }

        //----------------------------------------------------------------//

    }
}
