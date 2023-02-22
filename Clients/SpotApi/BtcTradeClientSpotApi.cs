using BtcTrade.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BtcTrade.Net.Clients.SpotApi
{
    public class BtcTradeClientSpotApi : RestApiClient
    {
        #region fields

        internal BtcTradeClientOptions Options { get; }
        

        #endregion

        #region Api clients

        /// <inheritdoc />
        public BtcTradeClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public BtcTradeClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public BtcTradeClientSpotApiTrading Trading { get; }

        /// <inheritdoc />
        public string ExchangeName => "BtcTrade";
        #endregion

        #region ctor
        internal BtcTradeClientSpotApi(Log log, BtcTradeClientOptions options)
            : base(log, options, options.SpotApiOptions)
        {
            Options = options;

            Account = new BtcTradeClientSpotApiAccount(this);
            ExchangeData = new BtcTradeClientSpotApiExchangeData(this);
            Trading = new BtcTradeClientSpotApiTrading(this);

            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.Array;
        }
        #endregion

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BtcTradeAuthenticationProvider(credentials, Options.NonceProvider ?? new BtcTradeNonceProvider());

        internal Uri GetUrl(string endpoint)
        {
            return new Uri($"{BaseAddress}{endpoint}");
        }

        internal async Task<WebCallResult<T>> SendRequestInternalAsync<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            var result = await SendRequest<T>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight, ignoreRateLimit: ignoreRateLimit).ConfigureAwait(false);
            
            //if (!result && result.Error!.Code == 500 && signed)
            //{
            //    await Account.GetApiTokenAsync();
            //}
            return result;
        }

        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, Options.SpotApiOptions.AutoTimestamp, Options.SpotApiOptions.TimestampRecalculationInterval, new TimeSyncState("BtcTrade Api") { LastSyncTime = DateTime.UtcNow });

        public override TimeSpan GetTimeOffset() => TimeSpan.Zero;


        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => Task.FromResult(new WebCallResult<DateTime>(null, null, null, null, null, null, null, null, DateTime.UtcNow, null));

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

        protected Task<WebCallResult<T>> SendRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken,
            Dictionary<string, object>? parameters = null, bool signed = false, HttpMethodParameterPosition? postPosition = null,
            ArrayParametersSerialization? arraySerialization = null, int weight = 1, bool ignoreRateLimit = false) where T : class
        {
            return base.SendRequestAsync<T>(uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, requestWeight: weight, ignoreRatelimit: ignoreRateLimit);
        }
    }
}
