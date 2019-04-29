using System.Data;
using ControlBot.DAL.Abstract;
using ControlBot.DAL.Concrety;

namespace ControlBot.DAL.Factories
{
    public class SessionFactory : ISessionFactory
    {
        public ISession CreateSession(IsolationLevel level) => new Session(level);
    }
}
