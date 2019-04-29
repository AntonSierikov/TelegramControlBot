using System;
using ControlBot.Core.Abstract;
using ControlBot.Core.Enums;

namespace ControlBot.Core.Entities
{
    public class Case : DbObject<Int32>
    {   
        public Int64  ChatId { get; set; }

        public String CaseName { get; private set; }

        public Int32? NextUserId { get; set; }

        public ControlUser NextUser { get; set; }

        public ScheduleType ScheduleType { get; set; }

        public TimeSpan TimeOfDay { get; set; }

        //----------------------------------------------------------------//

        public Case() {}

        //----------------------------------------------------------------//
            
        public Case(String name, TimeSpan timeOfDay, Int64 chatId)
        {
            CaseName = name;
            TimeOfDay = timeOfDay;
            ChatId = chatId;
        }

        //----------------------------------------------------------------//

    }
}
