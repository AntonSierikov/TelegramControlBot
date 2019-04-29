using System;
using Newtonsoft.Json;

namespace ControlBot.BL.Models
{
    public class TunnelInfo
    {

        //----------------------------------------------------------------//

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("uri")]
        public String URI { get; set; }

        [JsonProperty("public_url")]
        public String PublicURL { get; set; }
        
        [JsonProperty("proto")]
        public String Proto { get; set; }

        //----------------------------------------------------------------//

    }
}
