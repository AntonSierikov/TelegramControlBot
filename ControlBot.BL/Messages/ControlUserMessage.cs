using System;
using System.Collections.Generic;
using System.Text;

namespace ControlBot.BL.Messages
{
    public static class ControlUserMessage
    {

        //----------------------------------------------------------------//

        public static String UserExist(Int32 userId) => $"User with {userId} already exist";

        public static String UserAdded(Int32 userId) => $"User with {userId} was added";

    }
}
