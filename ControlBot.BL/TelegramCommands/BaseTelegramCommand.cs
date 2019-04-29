using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using ControlBot.DAL.Abstract;


namespace ControlBot.BL.TelegramCommands
{
    public abstract class BaseTelegramCommand
    {

        //----------------------------------------------------------------//

        public String Pattern { get; }

        //----------------------------------------------------------------//

        protected readonly Regex CommandRegex = null;

        //----------------------------------------------------------------//

        protected readonly ISessionFactory SessionFactory;
        protected readonly ICommandFactory CommandFactory;
        protected readonly IQueryFactory QueryFactory;
        protected readonly ITelegramBotClient BotClient;
        protected readonly IServiceProvider Provider;


        //----------------------------------------------------------------//

        public BaseTelegramCommand(IServiceProvider serviceProvider, String pattern)
        {
            Provider = serviceProvider;
            BotClient = serviceProvider.GetService<ITelegramBotClient>();
            SessionFactory = serviceProvider.GetService<ISessionFactory>();
            CommandFactory = serviceProvider.GetService<ICommandFactory>();
            QueryFactory = serviceProvider.GetService<IQueryFactory>();
            Pattern = pattern;
            CommandRegex = new Regex(pattern);
        }

        //----------------------------------------------------------------//

        public abstract Task<Message> ExecuteAsync(Message message);

        //----------------------------------------------------------------//

        public String[] GetCommandGroups(String text)
        {
            Match match = CommandRegex.Match(text);
            if (match.Success)
            {
                return match.Groups.Select(c => c.Value).ToArray();
            }

            return new String[0];
        }

        //----------------------------------------------------------------//

    }
}
