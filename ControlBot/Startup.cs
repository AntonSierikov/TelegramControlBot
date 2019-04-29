using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using ControlBot.Core.Constants;
using ControlBot.BL;
using ControlBot.BL.Models;
using ControlBot.BL.Launcher;

namespace ControlBot
{
    public class Startup
    {
        private HookUrlLauncher _launcher;
        private ICollection<String> Addresses;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            TelegramBotClient botClient = new TelegramBotClient(Configuration.GetValue<String>(GeneralBotConstants.BOT_TOKEN));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<ITelegramBotClient>(botClient);
            BLConfiguration.Configure(Configuration, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            Addresses = app.ServerFeatures.Get<IServerAddressesFeature>().Addresses;
            lifetime.ApplicationStarted.Register(() => OnStart(app.ApplicationServices));
            lifetime.ApplicationStopped.Register(OnShutDown);
            app.UseHttpsRedirection();
            app.UseMvc();
        }


        //----------------------------------------------------------------//
            
        private void OnStart(IServiceProvider provider)
        {

            _launcher = new HookUrlLauncher(Configuration);
            Uri appUri = new Uri(Addresses.First());
            try
            {
                _launcher.CreateTunnel(appUri.Port);
                ITelegramBotClient botClient = provider.GetRequiredService<ITelegramBotClient>();
                TunnelListResource listResource = _launcher.GetTunnelInfo().Result;
                String httpForw = listResource.Tunnels.First(t => t.Proto.Equals(CommonConstants.HTTPS)).PublicURL;
                botClient.SetWebhookAsync($"{httpForw}/api/bot/message/update/");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //----------------------------------------------------------------//

        private void OnShutDown()
        {
            _launcher.StopTunnel();
        }


        //----------------------------------------------------------------//

    }
}
