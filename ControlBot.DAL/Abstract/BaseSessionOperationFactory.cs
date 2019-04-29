using System;
using System.Collections.Generic;
using System.Text;

namespace ControlBot.DAL.Abstract
{
    public abstract class SessionOperationFactory
    {

        //----------------------------------------------------------------//

        private Dictionary<Type, Func<ISession, object>> _sessionOperations;

        //----------------------------------------------------------------//

        public SessionOperationFactory()
        {
            _sessionOperations = new Dictionary<Type, Func<ISession, object>>();
            InitSessionOperations();
        }

        //----------------------------------------------------------------//

        protected abstract void InitSessionOperations();

        //----------------------------------------------------------------//

        protected void AddSessionOperation<TInterface>(Func<ISession, object> func)
        {
            _sessionOperations.Add(typeof(TInterface), s => func(s));
        }

        //----------------------------------------------------------------//
        public T Create<T>(ISession session)
        {
            if(_sessionOperations.TryGetValue(typeof(T), out Func<ISession, object> func))
            {
                return (T)func(session);
            }

            throw new ArgumentException("Object not exist");
        }

        //----------------------------------------------------------------//
    }
}
