using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ControlBot.Core.Enums;
using ControlBot.BL.IServices;
using ControlBot.BL.Messages;

namespace ControlBot.BL.TelegramCommands
{
    public class CreateSpecificCaseCommand : CaseTimeBasedCommand
    {
        public const String _pattern = @"^\/(\w*) (\w*) ([0-3][0-9]-[0-1][0-2]-[0-2][0-9][0-9][0-9]) (([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9])$";

        //----------------------------------------------------------------//

        public CreateSpecificCaseCommand(IServiceProvider serviceProvider) 
            : base(serviceProvider, ScheduleType.ConcretyDate, _pattern)
        {}

        //----------------------------------------------------------------//

        public override async Task<String> CreateCase(IList<String> commandArgs, Int64 chatId, TimeSpan timeSpan)
        {
            String s_dateTime = commandArgs[3];
            String time = commandArgs[4];

            if(DateTime.TryParse(s_dateTime, out DateTime dateTime))
            {
                return await Provider.GetService<ICaseService>().CreateConcretyDateCaseAsync(dateTime, timeSpan, GetNameCase(commandArgs), chatId);
            }
            else return GeneralMessage.DATE_INCORRECT_FORMAT;
        }

        //----------------------------------------------------------------//

        public override string GetTime(IList<String> commandArgs) => commandArgs[4];

        //----------------------------------------------------------------//

    }
}
