using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using ControlBot.Core.Entities;
using ControlBot.DAL.Helpers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;
using ControlBot.DAL.IQueries;
using ControlBot.BL.Messages;

namespace ControlBot.BL.TelegramCommands
{
    internal class RegisterUserCommand : BaseTelegramCommand
    {
        public const String _pattern = @"$\/(\w*)"; 

        public RegisterUserCommand(IServiceProvider serviceProvider) 
            : base(serviceProvider, _pattern)
        {}

        //----------------------------------------------------------------//

        public override async Task<Message> ExecuteAsync(Message message)
        {
            User sender = message.From;
            Boolean isAdded = false;
            ControlUser user = new ControlUser(sender.Id, sender.FirstName, sender.LastName, sender.Username);
            using(ISession session = SessionFactory.CreateSession())
            {
                ICommand<ControlUser, Int32> userCommand = CommandFactory.CreateCommand<ICommand<ControlUser, Int32>>(session);
                IUserQuery userQuery = QueryFactory.CreateQuery<IUserQuery>(session);
                isAdded = await QueryHelper.SaveIfNotExist(userCommand, userQuery, user);
            }

            String resultMessage = isAdded ? ControlUserMessage.UserAdded(sender.Id) : ControlUserMessage.UserExist(sender.Id); 
            return await BotClient.SendTextMessageAsync(message.Chat.Id, resultMessage, replyToMessageId: message.MessageId);
        }

        //----------------------------------------------------------------//

    }
}
