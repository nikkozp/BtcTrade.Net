using BtcTrade.Net.Converts;
using BtcTrade.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeOpenOrder
    {
        [JsonProperty("id")]
        public long OrderId { get; private set; }

        [JsonProperty("type"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; private set; }

        [JsonProperty("status"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; private set; }

        [JsonProperty("unixtime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; private set; }

        [JsonProperty("price")]
        public decimal Price { get; private set; }

        public decimal Quantity => amnt_trade;
        public decimal QuoteQuantity => amnt_base;

        public decimal QuantityNotFilled => (Side == OrderSide.Buy) ? sum2 : sum1;
        public decimal QuoteQuantityNotFilled => (Side == OrderSide.Sell) ? sum2 : sum1;



        [JsonProperty("amnt_base")]
        private decimal amnt_base { get; set; }
        [JsonProperty("amnt_trade")]
        private decimal amnt_trade { get; set; }
        [JsonProperty("sum2"), JsonConverter(typeof(DecimalExponentialConverter))]
        private decimal sum2 { get; set; }
        [JsonProperty("sum1"), JsonConverter(typeof(DecimalExponentialConverter))]
        private decimal sum1 { get; set; }

    }
}
