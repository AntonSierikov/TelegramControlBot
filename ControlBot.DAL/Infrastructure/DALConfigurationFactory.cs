using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ControlBot.DAL.Constants;

namespace ControlBot.DAL.Infrastructure
{
    public static class DALConfigurationFactory
    {

        //----------------------------------------------------------------//

        public static String MainDbConnectionString{ get; private set; }

        //----------------------------------------------------------------//

        public static void Init(IConfiguration configuration)
        {
            MainDbConnectionString = configuration.GetConnectionString(SettingsConstants.MAIN_DB);
        }

        //----------------------------------------------------------------//


    }
}
