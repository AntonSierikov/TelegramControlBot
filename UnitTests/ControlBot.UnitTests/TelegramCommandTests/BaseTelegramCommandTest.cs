using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Telegram.Bot;
using Telegram.Bot.Types;
using ControlBot.BL.TelegramCommands;
using ControlBot.BL.Factories;
using ControlBot.UnitTests.Providers;
using ControlBot.UnitTests.ServiceMocks;

namespace ControlBot.UnitTests.TelegramCommandTests
{
    public class BaseTelegramCommandTest
    {
        protected Chat Chat { get; private set; }

        protected ServiceProviderMock ServiceProviderMock { get; private set; }

        //----------------------------------------------------------------//

        public BaseTelegramCommandTest()
        {
            Chat = new Chat();
            ServiceProviderMock = new ServiceProviderMock();
        }

        //----------------------------------------------------------------//

        public async Task TestTelegramCommand(String command, Message expectedMessage)
        {
            ServiceProviderMock.TryAddServiceMock<ITelegramBotClient>(new BotClientMock().SetupSendMessage(expectedMessage));

            TelegramCommandFactory telegramCommandFactory = new TelegramCommandFactory(ServiceProviderMock.Provider);
            BaseTelegramCommand telegramCommand = telegramCommandFactory.GetTelegramCommand(command);
            Message inputMessage = new Message() { Text = command, Chat = Chat };
            Message resultMessage = null;

            //assert
            try
            {
                resultMessage = await telegramCommand.ExecuteAsync(inputMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Assert.Equal(resultMessage?.Text, expectedMessage.Text);
        }

        //----------------------------------------------------------------//

    }
}
