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
    public class CreateSpecificDayCommandTest : BaseTelegramCommandTest
    {
        [Fact]
        public async Task CreateSpecificDayCase_Correct()
        {
            //arrange 
            String s_dateTime = "22-01-2018 17:30";
            String nameCase = "specific_case_test";
            String command = String.Join(StringConstants.SPACE, TelegramCommandConstants.CREATE_SPECIFIC_DATE_CASE, nameCase, s_dateTime);

            Message expectedMessage = new Message() { Text = CaseMessages.CaseAdded(ScheduleType.ConcretyDate, nameCase, s_dateTime) };

            ServiceProviderMock.TryAddServiceMock<ICaseService>(new CaseServiceMock().SetupConcretyDateCase(String.Empty));

            await TestTelegramCommand(command, expectedMessage);
        }
    }
}
