using ControlBot.Core.Constants;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;


namespace ControlBot.BL.TelegramCommands
{
    public class ConfirmCaseRequestCommand : BaseTelegramCommand
    {
        private const String _historyCasePattern = @"^(№\d+). (\w*)$";

        //----------------------------------------------------------------//
            
        public ConfirmCaseRequestCommand(IServiceProvider serviceProvider) 
            : base(serviceProvider, TelegramCommandConstants.MAKE_CASE)
        {}

        //----------------------------------------------------------------//
        
        public override async Task<Message> ExecuteAsync(Message message)
        {
            String status = null;

            if (message != null)
            {
                Int32 historyId = 0;
                using (ISession session = SessionFactory.CreateSession())
                {
                    IHistoryCommand historyCommand = CommandFactory.CreateCommand<IHistoryCommand>(session);
                    if (await historyCommand.SetHistoryAsSuccess(historyId))
                    {
                        status = CommonConstants.SUCCESS;
                    }
                }
            }
            else status = CommonConstants.FUCK_YOU;

            return await BotClient.SendTextMessageAsync(message.Chat.Id, status, replyToMessageId: message.MessageId);
        }

        //----------------------------------------------------------------//
    }
}
