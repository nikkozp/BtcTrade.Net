using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeOpenOrders
    {
        [JsonProperty("auth")]
        public bool Success { get; set; }

        [JsonProperty("balance_buy")]
        public decimal FreeBuy { get; set; }

        [JsonProperty("balance_sell")]
        public decimal FreeSell { get; set; }

        [JsonProperty("your_open_orders")]
        public List<BtcTradeOpenOrder> OpenOrders { get; set; }
    }
}
