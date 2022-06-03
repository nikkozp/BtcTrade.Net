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
            SpotApi = AddApiClient(new BtcTradeClientSpotApi(this, options));
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

        protected override Error ParseErrorResponse(JToken error)
        {
            try
            {
                if (error["auth"] != null && (bool)(error["auth"]) == false && error["description"] == null)
                    return new ServerError(0, "Невозможно выполнить оперцию. Нужна авторизация!");

                return new ServerError(0, (string)error["description"]);
            }
            catch
            {
                return new ServerError(0, JsonConvert.SerializeObject(error));
            }
        }

        internal Task<WebCallResult<T>> SendRequestInternal<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            return base.SendRequestAsync<T>(apiClient, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit);
        }
    }
}
