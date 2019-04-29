using System.Data;

namespace ControlBot.DAL.Abstract
{
    public interface ISessionFactory
    {
        ISession CreateSession(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
