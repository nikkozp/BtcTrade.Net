using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeWallet
    {
        [JsonProperty("msg_count")]
        public int MassageCount { get; set; }

        [JsonProperty("notify_count")]
        public int NotifyCount { get; set; }

        [JsonProperty("accounts")]
        public List<BtcTradeBalanceFree> Accounts { get; set; }
    }
}
