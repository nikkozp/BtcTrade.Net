using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradePlaceOrder
    {
        [JsonProperty("status")]
        public bool Success { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; set; }
    }
}
