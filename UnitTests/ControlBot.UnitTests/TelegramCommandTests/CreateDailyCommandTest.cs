using System;
using System.Threading.Tasks;
using Xunit;
using Telegram.Bot.Types;
using ControlBot.Core.Enums;
using ControlBot.Core.Constants;
using ControlBot.BL.IServices;
using ControlBot.BL.Messages;
using ControlBot.UnitTests.ServiceMocks;
using ControlBot.UnitTests.Providers;

namespace ControlBot.UnitTests.TelegramCommandTests
{
    public class CreateDailyCommandTest : BaseTelegramCommandTest
    {
        //----------------------------------------------------------------//

        [Fact]
        public async Task CreateDailyCase_Correct()
        {
            //arrange
            String time = "18:30";
            String nameCase = "daily_case_test";
            String command = String.Join(StringConstants.SPACE, TelegramCommandConstants.CREATE_DAILY_CASE, nameCase, time);

            //Response message
            Message expectedMessage = new Message() { Text = CaseMessages.CaseAdded(ScheduleType.Daily, nameCase, time)};

            //Mocks
            ServiceProviderMock.TryAddServiceMock<ICaseService>(new CaseServiceMock().SetupDailyCase(String.Empty));

            await TestTelegramCommand(command, expectedMessage);
        }

        //----------------------------------------------------------------//
    }
}
