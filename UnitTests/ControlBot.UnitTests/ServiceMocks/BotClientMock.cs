using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ControlBot.UnitTests.ServiceMocks
{
    internal class BotClientMock : BaseServiceMock<ITelegramBotClient>
    {
        public BotClientMock SetupSendMessage(Message message)
        {
            _serviceMock.Setup(c => c.SendTextMessageAsync(It.IsAny<Chat>(), It.IsAny<String>(), ParseMode.Default, 
                                                           false, false, 0, null, default(CancellationToken)))
                        .Returns(Task.FromResult(message));

            _serviceMock.Setup(c => c.SendTextMessageAsync(It.IsAny<ChatId>(), It.IsAny<String>(), ParseMode.Default,
                                               false, false, 0, null, default(CancellationToken)))
                        .Returns(Task.FromResult(message));
            return this;
        }
    }
}
