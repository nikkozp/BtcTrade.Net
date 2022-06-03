using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeTicker
    {
        [JsonProperty("currency_trade")]
        public string BaseSymbol { get; set; }

        [JsonProperty("currency_base")]
        public string QuoteSymbol { get; set; }

        [JsonProperty("usd_rate")]
        public decimal Rate { get; set; }
    }
}
