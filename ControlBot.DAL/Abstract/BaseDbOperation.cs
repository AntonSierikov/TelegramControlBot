using System;
using System.Data;
using ControlBot.DAL.Factories;

namespace ControlBot.DAL.Abstract
{
    public class BaseDbOperation<T>
    {
        protected ISession Session { get; private set; }

        protected IDbConnection Connection { get; private set; }

        protected IDbTransaction Transaction { get; private set; }

        protected String TableName { get; private set; }

        //----------------------------------------------------------------//

        public BaseDbOperation(ISession session)
        {
            Session = session;
            TableName = $"public.{TableFactory.GetNameTable<T>()}";
            Connection = session.Connection;
            Transaction = session.Transaction;
        }

        //----------------------------------------------------------------//

    }
}
