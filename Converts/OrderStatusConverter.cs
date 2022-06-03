using BtcTrade.Net.Enums;
using CryptoExchange.Net.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Converts
{
    public class OrderStatusConverter : BaseConverter<OrderStatus>
    {
        public OrderStatusConverter() : this(true) { }
        public OrderStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderStatus, string>> Mapping => new List<KeyValuePair<OrderStatus, string>>
        {
            new KeyValuePair<OrderStatus, string>(OrderStatus.Pending, "created"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.New, "processing"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Filled, "processed" ),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "canceled"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "canceling"),
            new KeyValuePair<OrderStatus, string>(OrderStatus.Rejected, "core_error")
        };
    }
}
