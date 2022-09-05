using BtcTrade.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BtcTrade.Net.Clients.SpotApi
{
    public class BtcTradeClientSpotApi : RestApiClient
    {
        private readonly BtcTradeClient _baseClient;
        private readonly Log _log;

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
        internal BtcTradeClientSpotApi(Log log, BtcTradeClient baseClient, BtcTradeClientOptions options)
            : base(options, options.SpotApiOptions)
        {
            _log = log;
            Options = options;
            _baseClient = baseClient;

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
            var result = await _baseClient.SendRequestInternal<T>(this, uri, method, cancellationToken, parameters, signed, postPosition, arraySerialization, weight, ignoreRateLimit: ignoreRateLimit).ConfigureAwait(false);
            //if (!result && result.Error!.Code == -1021 && Options.SpotApiOptions.AutoTimestamp)
            //{
            //    _log.Write(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
            //    TimeSyncState.LastSyncTime = DateTime.MinValue;
            //}
            return result;
        }

        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, Options.SpotApiOptions.AutoTimestamp, Options.SpotApiOptions.TimestampRecalculationInterval, new TimeSyncState("BtcTrade Api") { LastSyncTime = DateTime.UtcNow });

        public override TimeSpan GetTimeOffset() => TimeSpan.Zero;


        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => Task.FromResult(new WebCallResult<DateTime>(null, null, null, null, null, null, null, null, DateTime.UtcNow, null));
    }
}
