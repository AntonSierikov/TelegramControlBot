using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using ControlBot.BL.Abstract;
using ControlBot.BL.Helpers;
using ControlBot.BL.Messages;
using ControlBot.BL.TelegramCommands;

namespace ControlBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ITelegramCommandFactory _commandFactory;

        public BotController(ITelegramBotClient botClient, ITelegramCommandFactory commandFactory)
        {
            _botClient = botClient;
            _commandFactory = commandFactory;
        }

        //----------------------------------------------------------------//

        [Route("message/update")]
        public async Task<ActionResult> Update([FromBody]Update update)
        {
            Message message = update.Message ?? update.EditedMessage;
            DateTime nearTime = DateTime.Now.AddMinutes(-5.0).ToUniversalTime();

            if(message.Date < nearTime)
            {
                return new BadRequestResult();
            }
            else if (TelegramCommandHelper.TryParse(message.Text, out String command))
            {
                BaseTelegramCommand baseCommand = _commandFactory.GetTelegramCommand(command);
                try
                {
                    Message resultMessage = await baseCommand.ExecuteAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, GeneralMessage.COMMAND_NOT_MATCH_PATTERN, replyToMessageId: message.MessageId);
            }

            return Ok();
        }

        //----------------------------------------------------------------//


    }
}