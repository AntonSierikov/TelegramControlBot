using System;
using System.Threading.Tasks;
using System.Linq;
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
    public class CreateWeeklyCommandTest : BaseTelegramCommandTest
    {

        //----------------------------------------------------------------//
            
        [Fact]
        public async Task CreateWeeklyCase_Correct()
        {
            //arrange
            String time = "05:30";
            String nameCase = "week_case_test";
            String[] daysOfWeek = new String[] { nameof(DayOfWeek.Monday), nameof(DayOfWeek.Tuesday), nameof(DayOfWeek.Monday) };
            String s_days = String.Join(StringConstants.SPACE, daysOfWeek);
            String command = String.Join(StringConstants.SPACE, TelegramCommandConstants.CREATE_WEEKLY_CASE, nameCase, s_days, time);

            //Response message
            Message expectedMessage = new Message() { Text = CaseMessages.CaseAdded(ScheduleType.Weekly, nameCase, time) };

            //Mocks
            ServiceProviderMock.TryAddServiceMock<ICaseService>(new CaseServiceMock().SetupWeeklyCase(String.Empty));

            await TestTelegramCommand(command, expectedMessage);
        }

    }
}
