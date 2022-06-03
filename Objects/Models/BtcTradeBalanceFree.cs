using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeBalanceFree
    {
        [JsonProperty("currency")]
        public string Asset { get; set; }

        [JsonProperty("balance")]
        public decimal Free { get; set; }
    }
}
