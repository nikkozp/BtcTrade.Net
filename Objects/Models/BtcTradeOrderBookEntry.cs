using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeOrderBookEntry
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("currency_trade")]
        public decimal Quantity { get; set; }
        [JsonProperty("currency_base")]
        public decimal QuoteQuantity { get; set; }
    }
}
