using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ControlBot.Core.Enums;
using ControlBot.BL.IServices;

namespace ControlBot.BL.TelegramCommands
{
    public class CreateDailyCommand : CaseTimeBasedCommand
    {
        private const String _pattern = @"^\/(\w*) (\w*) (([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9])$";

        //----------------------------------------------------------------//

        private readonly ICaseService _caseService;

        //----------------------------------------------------------------//

        public CreateDailyCommand(IServiceProvider serviceProvider)
            : base(serviceProvider, ScheduleType.Daily, _pattern)
        {
            _caseService = serviceProvider.GetService<ICaseService>();
        }

        //----------------------------------------------------------------//

        public override Task<String> CreateCase(IList<String> commandArgs, Int64 chatId, TimeSpan timeSpan)
        {
            String nameCase = GetNameCase(commandArgs);
            return _caseService.CreateDailyCaseAsync(timeSpan, nameCase, chatId);
        }

        //----------------------------------------------------------------//

        public override string GetTime(IList<String> commandArgs) => commandArgs[3];

        //----------------------------------------------------------------//

    }
}
