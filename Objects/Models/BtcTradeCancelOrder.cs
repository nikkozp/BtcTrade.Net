using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeCancelOrder
    {
        [JsonProperty("status")]
        public bool Success { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
