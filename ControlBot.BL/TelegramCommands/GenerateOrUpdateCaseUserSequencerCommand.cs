using ControlBot.BL.IServices;
using ControlBot.BL.Messages;
using ControlBot.BL.Models;
using ControlBot.Core.Constants;
using ControlBot.DAL.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Linq;
using Telegram.Bot.Types;

namespace ControlBot.BL.TelegramCommands
{
    public class CreateCaseUserSequencerCommand : BaseTelegramCommand
    {
        private const String _pattern = @"^\/(\w*) (\w*)";

        public CreateCaseUserSequencerCommand(IServiceProvider serviceProvider) 
            : base(serviceProvider, _pattern)
        {
        }

        public override async Task<Message> ExecuteAsync(Message inputMessage)
        {
            ISequencerService sequencerService = Provider.GetRequiredService<ISequencerService>();

            String[] groups = GetCommandGroups(inputMessage.Text);
            String outputMessage = null;

            if(groups.Length > 0)
            {
                String[] usersName = inputMessage.Text.Replace(groups[0], String.Empty)
                                                      .Replace(StringConstants.DOG_SYMBOL.ToString(), String.Empty)
                                                      .Split(StringConstants.SPACE)
                                                      .Where(u => !String.IsNullOrWhiteSpace(u)).ToArray();

                using (ISession session = SessionFactory.CreateSession())
                {
                    ProcessResult result = await sequencerService.TryGenerateOrUpdateSequence(session, groups[2], usersName);
                    outputMessage = result.Message;
                    
                    if (result.IsSuccess)
                    {
                        Task reinitNotifications = Provider.GetRequiredService<INotificationService>().ReinitNotificationList();
                    }
                }
            }
            else outputMessage = GeneralMessage.COMMAND_NOT_MATCH_PATTERN;

            return await BotClient.SendTextMessageAsync(inputMessage.Chat.Id, outputMessage, replyToMessageId: inputMessage.MessageId);
        }
    }
}
