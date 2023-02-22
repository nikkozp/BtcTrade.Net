using BtcTrade.Net.Clients.SpotApi;
using BtcTrade.Net.Enums;
using BtcTrade.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BtcTrade.Net.Clients
{
    public class BtcTradeClient : BaseRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public BtcTradeClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor
        /// <summary>
        /// Create a new instance of BinanceClient using the default options
        /// </summary>
        public BtcTradeClient() : this(BtcTradeClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of BinanceClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BtcTradeClient(BtcTradeClientOptions options) : base("BtcTrade", options)
        {
            SpotApi = AddApiClient(new BtcTradeClientSpotApi(log, options));
        }
        #endregion

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="options">Options to use as default</param>
        public static void SetDefaultOptions(BtcTradeClientOptions options)
        {
            BtcTradeClientOptions.Default = options;
        }
    }
}
