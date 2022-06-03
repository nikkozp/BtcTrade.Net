using BtcTrade.Net.Converts;
using BtcTrade.Net.Enums;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects.Models
{
    public class BtcTradeQueryOrder
    {
        [JsonProperty("Id")]
        public long OrderId { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(OrderSideConverter))]
        public OrderSide Side { get; set; }

        [JsonProperty("status"), JsonConverter(typeof(OrderStatusConverter))]
        public OrderStatus Status { get; set; }

        [JsonProperty("unixtime"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("price"), JsonConverter(typeof(DecimalExponentialConverter))]
        public decimal Price { get; set; }


        public decimal Quantity => (Side == OrderSide.Buy) ? sum2_history : sum1_history;
        public decimal QuoteQuantity => (Side == OrderSide.Sell) ? sum2_history : sum1_history;

        public decimal QuantityNotFilled => (Side == OrderSide.Buy) ? sum2 : sum1;
        public decimal QuoteQuantityNotFilled => (Side == OrderSide.Sell) ? sum2 : sum1;








        [JsonProperty("sum2_history"), JsonConverter(typeof(DecimalExponentialConverter))]
        private decimal sum2_history { get; set; }

        [JsonProperty("sum2"), JsonConverter(typeof(DecimalExponentialConverter))]
        private decimal sum2 { get; set; }

        [JsonProperty("sum1_history"), JsonConverter(typeof(DecimalExponentialConverter))]
        private decimal sum1_history { get; set; }

        [JsonProperty("sum1"), JsonConverter(typeof(DecimalExponentialConverter))]
        private decimal sum1 { get; set; }
    }
}
