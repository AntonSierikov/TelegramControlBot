using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.BL.IServices;
using ControlBot.BL.Messages;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;
using ControlBot.DAL.IQueries;
using ControlBot.DAL.Helpers;

namespace ControlBot.BL.Services
{
    internal class CaseService : BaseService, ICaseService
    {
        //----------------------------------------------------------------//

        private Func<Boolean, String> _caseExist = isAdded => isAdded ? String.Empty : CaseMessages.CASE_ALREADY_EXISTS;

        //----------------------------------------------------------------//

        public CaseService(IServiceProvider provider)
            :base(provider)
        {}

        //----------------------------------------------------------------//
       
        public async Task<String> CreateConcretyDateCaseAsync(DateTime dateTime, TimeSpan timeSpan, String name, Int64 chatId)
        {
            ConcretyDateCase concretyDate = new ConcretyDateCase(name, timeSpan, chatId);
            Boolean isAdded = false;

            using (ISession session = SessionFactory.CreateSession())
            {
                ICaseQuery caseQuery = QueryFactory.CreateQuery<ICaseQuery>(session);
                IQuery<CaseDate, CaseDateId> caseDateQuery = QueryFactory.CreateQuery<IQuery<CaseDate, CaseDateId>>(session);
                ICommand<Case, Int32> caseCommand = CommandFactory.CreateCommand<ICommand<Case, Int32>>(session);
                ICommand<CaseDate, CaseDateId> caseDateCommand = CommandFactory.CreateCommand<ICommand<CaseDate, CaseDateId>>(session);

                isAdded = await caseCommand.SaveIfNotExist(caseQuery, concretyDate) && 
                          await caseDateCommand.SaveIfNotExist(caseDateQuery, new CaseDate(concretyDate, dateTime));
            }
            
            return _caseExist(isAdded);
        }

        //----------------------------------------------------------------//

        public async Task<String> CreateDailyCaseAsync(TimeSpan time, String name, Int64 chatId)
        {
            Case @case = new Case(name, time, chatId);

            using(ISession session = SessionFactory.CreateSession())
            {
                ICaseQuery caseQuery = QueryFactory.CreateQuery<ICaseQuery>(session);
                ICommand<Case, Int32> caseCommand = CommandFactory.CreateCommand<ICommand<Case, Int32>>(session);

                return _caseExist(await caseCommand.SaveIfNotExist(caseQuery, @case)); 
            }
        }

        //----------------------------------------------------------------//

        public async Task<String> CreateWeeklyCaseAsync(List<DayOfWeek> dayOfWeeks, TimeSpan time, String name, Int64 chatId)
        {
            WeeklyCase weeklyCase = new WeeklyCase(name, time, chatId, dayOfWeeks);
            Boolean isAdded = false;

            using (ISession session = SessionFactory.CreateSession())
            {
                ICaseQuery caseQuery = QueryFactory.CreateQuery<ICaseQuery>(session);
                ICommand<Case, Int32> caseCommand = CommandFactory.CreateCommand<ICommand<Case, Int32>>(session);
                IQuery<CaseDayOfWeek, CaseDayOfWeekId> caseDayOfWeekQuery = QueryFactory.CreateQuery<IQuery<CaseDayOfWeek, CaseDayOfWeekId>>(session);
                ICommand<CaseDayOfWeek, CaseDayOfWeekId> caseDayOfWeekCommand = CommandFactory.CreateCommand<ICommand<CaseDayOfWeek, CaseDayOfWeekId>>(session);

                isAdded = await caseCommand.SaveIfNotExist(caseQuery, weeklyCase);
                IEnumerable<CaseDayOfWeek> caseDayOfWeeks = dayOfWeeks.Select(c => new CaseDayOfWeek(weeklyCase, c));
                await caseDayOfWeekCommand.SaveIfNotExistCollection(caseDayOfWeekQuery, caseDayOfWeeks);
            }
            return _caseExist(isAdded);
        }

        //----------------------------------------------------------------//

    }
}
