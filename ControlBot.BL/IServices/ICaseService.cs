using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControlBot.BL.IServices
{
    public interface ICaseService
    {
        Task<String> CreateDailyCaseAsync(TimeSpan time, String name, Int64 chatId);

        Task<String> CreateWeeklyCaseAsync(List<DayOfWeek> dayOfWeeks, TimeSpan timem, String name, Int64 chatId);

        Task<String> CreateConcretyDateCaseAsync(DateTime dateTimes, TimeSpan timeSpan, String name, Int64 chatId);
    }
}
