using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeOrderBook
    {
        [JsonProperty("min_price")]
        public decimal MinPrice { get; set; }

        [JsonProperty("max_price")]
        public decimal MaxPrice { get; set; }

        [JsonProperty("orders_sum")]
        public decimal TotalQuantity { get; set; }

        [JsonProperty("list")]
        public List<BtcTradeOrderBookEntry> Entry { get; set; } = new List<BtcTradeOrderBookEntry>();
    }
}
