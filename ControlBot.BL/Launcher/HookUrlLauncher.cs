using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using ControlBot.Core.Constants;
using ControlBot.BL.Models;

namespace ControlBot.BL.Launcher
{
    public class HookUrlLauncher
    {
        private Process _ngrokProcess;
        private const Int32 StartNgrokTunnelMs = 300;

        //----------------------------------------------------------------//

        public String NgrokPath { get; }
        public String NgrokProcessName { get; }

        public String NgrokApiUrl { get;  }

        //----------------------------------------------------------------//

        public HookUrlLauncher(IConfiguration configuration)
        {
            NgrokPath = configuration.GetValue<String>(GeneralBotConstants.NGROK_PATH);
            NgrokProcessName = configuration.GetValue<String>(GeneralBotConstants.NGROK_PROCESS_NAME);
            NgrokApiUrl = configuration.GetValue<String>(GeneralBotConstants.NGROK_API_URL);
        }

        //----------------------------------------------------------------//

        public void CreateTunnel(Int32 appPort)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(NgrokPath, $"http {appPort}");
            startInfo.UseShellExecute = false;
            _ngrokProcess = new Process();
            _ngrokProcess.StartInfo = startInfo;
            _ngrokProcess.Exited += new EventHandler(ProcessExited);
            _ngrokProcess.Start();
        }

        private void ProcessExited(object sender, EventArgs e)
        {
            Console.WriteLine("Ngrok Exited");
        }

        //----------------------------------------------------------------//

        public void StopTunnel()
        {
            _ngrokProcess.Kill();
        }

        //----------------------------------------------------------------//

        public async Task<TunnelListResource> GetTunnelInfo()
        {
            HttpResponseMessage response = null;
            using (HttpClient client = new HttpClient())
            {
                response = await client.GetAsync($"{NgrokApiUrl}tunnels/");
            }

            String json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TunnelListResource>(json); 
        }

        //----------------------------------------------------------------//

    }
}
