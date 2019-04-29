using ControlBot.DAL.Factories;
using ControlBot.DAL.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace ControlBot.DAL
{
    public static class DALServiceRegister
    {
        public static void Register(IServiceCollection collection)
        {
            collection.AddSingleton<ISessionFactory, SessionFactory>();
            collection.AddSingleton<ICommandFactory, CommandFactory>();
            collection.AddSingleton<IQueryFactory, QueryFactory>();
        }
    }
}
