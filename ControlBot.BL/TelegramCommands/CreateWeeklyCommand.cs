using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ControlBot.Core.Constants;
using ControlBot.Core.Enums;
using ControlBot.BL.IServices;
using ControlBot.BL.Extensions;
using ControlBot.BL.Messages;
using Telegram.Bot.Types;

namespace ControlBot.BL.TelegramCommands
{
    public class CreateWeeklyCommand : CaseTimeBasedCommand
    {

        //----------------------------------------------------------------//

        private const String _pattern = @"$\/(\w*) (\w*) (([0-6] ){1,7}) (([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9])$";

        private readonly ICaseService _caseService;

        //----------------------------------------------------------------//

        public CreateWeeklyCommand(IServiceProvider provider)
            : base(provider, ScheduleType.Weekly, _pattern)
        {
            _caseService = provider.GetService<ICaseService>();
        }

        //----------------------------------------------------------------//

        public override async Task<String> CreateCase(IList<String> commandArgs, Int64 chatId, TimeSpan timeSpan)
        {
            String errorMsg = String.Empty;
            String[] s_days = commandArgs[3].Split(StringConstants.COMA_SPACE);
            List<String> notParsed;

            List<DayOfWeek> dayOfWeeks = s_days.AddRange((String str, out DayOfWeek day) => Enum.TryParse(str, out day), out notParsed);

            if(notParsed != null && notParsed.Count > 0)
            {
                errorMsg = CaseMessages.DayOfWeekNotParsed(notParsed);
            }
            else if (dayOfWeeks.Count > 0)
            {
                errorMsg = await _caseService.CreateWeeklyCaseAsync(dayOfWeeks, timeSpan, GetNameCase(commandArgs), chatId);
            }

            return errorMsg;
        }

        //----------------------------------------------------------------//

        public override string GetTime(IList<String> commandArgs) => commandArgs[4];

        //----------------------------------------------------------------//

    }
}
