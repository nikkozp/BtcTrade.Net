using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTrade.Net.Objects
{
    public class BtcTradeClientOptions : BaseRestClientOptions
    {
        public static BtcTradeClientOptions Default { get; set; } = new BtcTradeClientOptions();

        public INonceProvider? NonceProvider { get; set; }


        private RestApiClientOptions _spotApiOptions = new RestApiClientOptions(BtcTradeApiAddresses.Default.RestClientAddress)
        {
            //RateLimiters = new List<IRateLimiter>
            //{
            //        new RateLimiter()
            //            .AddApiKeyLimit(15, TimeSpan.FromSeconds(45), false, false)
            //            .AddEndpointLimit(new [] { "/private/AddOrder", "/private/CancelOrder", "/private/CancelAll", "/private/CancelAllOrdersAfter" }, 60, TimeSpan.FromSeconds(60), null, true),
            //}
        };

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions = new RestApiClientOptions(_spotApiOptions, value);
        }

        public BtcTradeClientOptions() : this(Default)
        {
        }

        internal BtcTradeClientOptions(BtcTradeClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            NonceProvider = baseOn.NonceProvider;
            _spotApiOptions = new RestApiClientOptions(baseOn.SpotApiOptions, null);
        }
    }
}
