using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using ControlBot.Core.Enums;
using ControlBot.BL.Abstract;
using ControlBot.BL.Messages;
using ControlBot.BL.IServices;

namespace ControlBot.Controllers
{
    [Route("api/daily")]
    [ApiController]
    public class DailyCaseController : ControllerBase
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ITelegramCommandFactory _commandFactory;
        private readonly IServiceProvider _provider;

        //----------------------------------------------------------------//

        public DailyCaseController(ITelegramBotClient botClient, ITelegramCommandFactory commandFactory)
        {
            _botClient = botClient;
            _commandFactory = commandFactory;
            _provider = HttpContext.RequestServices;
        }

        //----------------------------------------------------------------//

        public async Task<OkResult> CreateDaily([FromQuery]Int32 chatId)
        {
            await _botClient.SendTextMessageAsync(chatId, CaseMessages.SELECT_TIME);
            return Ok();
        }

        //----------------------------------------------------------------//

        public async Task<OkObjectResult> CreateOrUpdate(String s_time, Int64 chatId, String name)
        {
            TimeSpan time;
            if(TimeSpan.TryParse(s_time, out time))
            {
                await _provider.GetService<ICaseService>().CreateDailyCaseAsync(time, name, chatId);
                return Ok(CaseMessages.CaseAdded(ScheduleType.Daily, name, s_time));
            }
            else
            {
                return Ok(GeneralMessage.TIME_INCORRECT_FORMAT);
            }
        }

        //----------------------------------------------------------------//

    }
}
