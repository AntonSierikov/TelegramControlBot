using System;
using ControlBot.DAL.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace ControlBot.BL.Services
{
    internal class BaseService
    {

        //----------------------------------------------------------------//

        protected readonly IQueryFactory QueryFactory;

        protected readonly ISessionFactory SessionFactory;

        protected readonly ICommandFactory CommandFactory;

        protected readonly IServiceProvider ServiceProvider;

        //----------------------------------------------------------------//

        public BaseService(IServiceProvider provider)
        {
            ServiceProvider = provider;
            QueryFactory = provider.GetService<IQueryFactory>();
            SessionFactory = provider.GetService<ISessionFactory>();
            CommandFactory = provider.GetService<ICommandFactory>();
        }

        //----------------------------------------------------------------//

    }
}
