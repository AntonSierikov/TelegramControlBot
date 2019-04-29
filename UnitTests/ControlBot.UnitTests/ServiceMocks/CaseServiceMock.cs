using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ControlBot.BL.IServices;
using Moq;

namespace ControlBot.UnitTests.ServiceMocks
{
    internal class CaseServiceMock : BaseServiceMock<ICaseService>
    {
  
        //----------------------------------------------------------------//

        public CaseServiceMock SetupWeeklyCase(String result)
        {
            _serviceMock.Setup(c => c.CreateWeeklyCaseAsync(It.IsAny<List<DayOfWeek>>(), It.IsAny<TimeSpan>(), It.IsAny<String>()))
                        .Returns(Task.FromResult(result));
            return this;
        }


        //----------------------------------------------------------------//

        public CaseServiceMock SetupDailyCase(String result)
        {
            _serviceMock.Setup(c => c.CreateDailyCaseAsync(It.IsAny<TimeSpan>(), It.IsAny<String>()))
                        .Returns(Task.FromResult(result));
            return this;
        }

        //----------------------------------------------------------------//

        public CaseServiceMock SetupConcretyDateCase(String result)
        {
            _serviceMock.Setup(c => c.CreateConcretyDateCaseAsync(It.IsAny<DateTime>(), It.IsAny<TimeSpan>(), It.IsAny<String>()))
                        .Returns(Task.FromResult(result));
            return this;
        }

        //----------------------------------------------------------------//

    }
}
