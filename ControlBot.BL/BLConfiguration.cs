using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ControlBot.DAL.Infrastructure;
using ControlBot.DAL;
using ControlBot.BL.Abstract;
using ControlBot.BL.Factories;
using ControlBot.BL.IServices;
using ControlBot.BL.Services;

namespace ControlBot.BL
{
    public class BLConfiguration
    {
        public static void Configure(IConfiguration configuration, IServiceCollection collection)
        {
            DALConfigurationFactory.Init(configuration);
            ConfigureDependencies(collection);
        }

        //----------------------------------------------------------------//

        public static void ConfigureDependencies(IServiceCollection collection)
        {
            DALServiceRegister.Register(collection);
            collection.AddSingleton<ITelegramCommandFactory>(s => new TelegramCommandFactory(s));
            collection.AddSingleton<ICaseService, CaseService>(provider => new CaseService(provider));
            collection.AddSingleton<INotificationService, NotificationService>();
            collection.AddSingleton<ISequencerService, SequencerService>();
            collection.AddSingleton<IHistoryService, HistoryService>();
        }

        //----------------------------------------------------------------//

    }
}
