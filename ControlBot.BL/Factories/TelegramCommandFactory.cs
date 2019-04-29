using System;
using System.Collections.Generic;
using System.Text;
using ControlBot.Core.Constants;
using ControlBot.BL.Abstract;
using ControlBot.BL.TelegramCommands;
using ControlBot.BL.Helpers;

namespace ControlBot.BL.Factories
{
    public class TelegramCommandFactory : ITelegramCommandFactory
    {
        private readonly Dictionary<String, BaseTelegramCommand> _telegramCommands;
        private readonly IServiceProvider _serviceProvider;

        //----------------------------------------------------------------//

        public TelegramCommandFactory(IServiceProvider serviceProvider)
        {
            _telegramCommands = new Dictionary<String, BaseTelegramCommand>();
            _serviceProvider = serviceProvider;
            InitCommands();
        }

        //----------------------------------------------------------------//

        private void InitCommands()
        {
            _telegramCommands.Add(TelegramCommandConstants.REGISTER, new RegisterUserCommand(_serviceProvider));
            _telegramCommands.Add(TelegramCommandConstants.CREATE_DAILY_CASE, new CreateDailyCommand(_serviceProvider));
            _telegramCommands.Add(TelegramCommandConstants.CREATE_WEEKLY_CASE, new CreateWeeklyCommand(_serviceProvider));
            _telegramCommands.Add(TelegramCommandConstants.CREATE_SPECIFIC_DATE_CASE, new CreateSpecificCaseCommand(_serviceProvider));
            _telegramCommands.Add(TelegramCommandConstants.Update_Sequence, new CreateCaseUserSequencerCommand(_serviceProvider));
            _telegramCommands.Add(TelegramCommandConstants.MAKE_CASE, new ConfirmCaseRequestCommand(_serviceProvider));
        }

        //----------------------------------------------------------------//

        public BaseTelegramCommand GetTelegramCommand(String command)
        {
            return TelegramCommandHelper.TryParse(command, out String nameCommand)
                   ? _telegramCommands.GetValueOrDefault(nameCommand) : null;
        }

        //----------------------------------------------------------------//

    }
}
