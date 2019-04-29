using System;
using ControlBot.Core.Entities;
using ControlBot.Core.Indentifiers;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.IQueries;
using ControlBot.DAL.Queries;

namespace ControlBot.DAL.Factories
{
    public class QueryFactory : SessionOperationFactory, IQueryFactory
    {

        //----------------------------------------------------------------//

        public T CreateQuery<T>(ISession session)
        {
            return Create<T>(session);
        }

        //----------------------------------------------------------------//

        protected override void InitSessionOperations()
        {
            AddSessionOperation<IUserQuery>(s => new UserQuery(s));
            AddSessionOperation<ICaseQuery>(s => new CaseQuery(s));
            AddSessionOperation<IQuery<CaseDate, CaseDateId>>(s => new CaseConcretyDateQuery(s));
            AddSessionOperation<IQuery<CaseDayOfWeek, CaseDayOfWeekId>>(s => new CaseDayOfWeekQuery(s));
            AddSessionOperation<IQuery<UserCaseSequencer, UserCaseSequencerId>>(s => new UserCaseSequencerQuery(s));
        }

        //----------------------------------------------------------------//

    }
}
