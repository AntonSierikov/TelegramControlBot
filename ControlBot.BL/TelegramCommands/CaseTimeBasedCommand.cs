using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;
using ControlBot.Core.Enums;
using ControlBot.BL.Messages;
using ControlBot.BL.IServices;

namespace ControlBot.BL.TelegramCommands
{
    public abstract class CaseTimeBasedCommand : BaseTelegramCommand
    {
        private readonly ScheduleType scheduleType;

        //----------------------------------------------------------------//

        public CaseTimeBasedCommand(IServiceProvider serviceProvider, ScheduleType scheduleType, string pattern) 
            : base(serviceProvider, pattern)
        {
            this.scheduleType = scheduleType;
        }

        //----------------------------------------------------------------//

        public virtual String GetNameCase(IList<String> commandArgs) => commandArgs[2];

        //----------------------------------------------------------------//

        public abstract String GetTime(IList<String> commandArgs);

        //----------------------------------------------------------------//

        public abstract Task<String> CreateCase(IList<String> commandArgs, Int64 chatId, TimeSpan timeSpan);

        //----------------------------------------------------------------//

        public async override Task<Message> ExecuteAsync(Message message)
        {
            Task t_reinitNotifacationList = Task.CompletedTask;
            String resultMsg = null;

            String[] values = GetCommandGroups(message.Text);

            if (values.Length > 0)
            {
                String s_time = GetTime(values);
                String nameCase = GetNameCase(values);

                if (TimeSpan.TryParse(s_time, out TimeSpan timeSpan))
                {
                    resultMsg = await CreateCase(values, message.Chat.Id, timeSpan);
                    if (String.IsNullOrEmpty(resultMsg))
                    {
                        resultMsg = CaseMessages.CaseAdded(scheduleType, nameCase, s_time);
                        t_reinitNotifacationList = Provider.GetService<INotificationService>().ReinitNotificationList();
                    }
                }
                else resultMsg = GeneralMessage.TIME_INCORRECT_FORMAT;
            }
            else resultMsg = GeneralMessage.COMMAND_NOT_MATCH_PATTERN;

            Task<Message> t_sendMessage = BotClient.SendTextMessageAsync(message.Chat.Id, resultMsg, replyToMessageId: message.MessageId);
            return await t_sendMessage;
        }

        //----------------------------------------------------------------//

    }
}
