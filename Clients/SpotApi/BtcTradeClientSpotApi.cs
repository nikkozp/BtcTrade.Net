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

        #region fields

        internal BtcTradeClientOptions ClientOptions { get; }
        

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
        internal BtcTradeClientSpotApi(BtcTradeClient baseClient, BtcTradeClientOptions options)
            : base(options, options.SpotApiOptions)
        {
            ClientOptions = options;
            _baseClient = baseClient;

            Account = new BtcTradeClientSpotApiAccount(this);
            ExchangeData = new BtcTradeClientSpotApiExchangeData(this);
            Trading = new BtcTradeClientSpotApiTrading(this);

            requestBodyFormat = RequestBodyFormat.FormData;
            arraySerialization = ArrayParametersSerialization.Array;
        }
        #endregion

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BtcTradeAuthenticationProvider(credentials, ClientOptions.NonceProvider ?? new BtcTradeNonceProvider());

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

        protected override TimeSyncInfo GetTimeSyncInfo()
        {
            throw new NotImplementedException();
        }

        public override TimeSpan GetTimeOffset()
        {
            throw new NotImplementedException();
        }

        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
        {
            throw new NotImplementedException();
        }
    }
}
