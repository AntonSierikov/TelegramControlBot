using System;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.ICommands;
using ControlBot.DAL.Commands;

namespace ControlBot.DAL.Factories
{
    public class CommandFactory : SessionOperationFactory, ICommandFactory
    {

        //----------------------------------------------------------------//

        public T CreateCommand<T>(ISession session)
        {
            return Create<T>(session);
        }

        //----------------------------------------------------------------//

        protected override void InitSessionOperations()
        {
            AddSessionOperation<ICommand<ControlUser, Int32>>(s => new UserCommand(s));
            AddSessionOperation<ICommand<Case, Int32>>(s => new CaseCommand(s));
            AddSessionOperation<ICommand<CaseDate, CaseDateId>>(s => new CaseConcretyDateCommand(s));
            AddSessionOperation<ICommand<CaseDayOfWeek, CaseDayOfWeekId>>(s => new CaseDayOfWeekCommand(s));
            AddSessionOperation<ICommand<UserCaseSequencer, UserCaseSequencerId>>(s => new UserCaseSequencerCommand(s));
            AddSessionOperation<IHistoryCommand>(s => new NotificationHistoryCommand(s));
        }

        //----------------------------------------------------------------//
    }
}
