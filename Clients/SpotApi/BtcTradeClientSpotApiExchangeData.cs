using BtcTrade.Net.Enums;
using BtcTrade.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BtcTrade.Net.Clients.SpotApi
{
    public class BtcTradeClientSpotApiExchangeData
    {
        private readonly BtcTradeClientSpotApi _baseClient;

        //private const string TickerEndpoint = "ticker";
        //private const string OrderBookEndpoint = "trades/{}/{}";

        internal BtcTradeClientSpotApiExchangeData(BtcTradeClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        public WebCallResult<Dictionary<string, BtcTradeTicker>> GetTicker(CancellationToken ct = default) => GetTickerAsync(ct).Result;
        public async Task<WebCallResult<Dictionary<string, BtcTradeTicker>>> GetTickerAsync(CancellationToken ct = default)
        {
            WebCallResult<Dictionary<string, object>> ticker = await _baseClient.SendRequestInternalAsync<Dictionary<string, object>>(_baseClient.GetUrl("ticker"), HttpMethod.Get, ct).ConfigureAwait(false);

            Dictionary<string, BtcTradeTicker> data = null;

            if (ticker.Data != null)
            {
                data = new Dictionary<string, BtcTradeTicker>();

                foreach (KeyValuePair<string, object> symbol in ticker.Data)
                {
                    if (symbol.Key == "status")
                        continue;

                    data.Add(symbol.Key, JsonConvert.DeserializeObject<BtcTradeTicker>(symbol.Value.ToString()));

                }
                ticker.Data.Remove("status");
            }

            return new WebCallResult<Dictionary<string, BtcTradeTicker>>(ticker.ResponseStatusCode, ticker.ResponseHeaders, null, null, null, null, null, null, data, ticker.Error);
        }

        public WebCallResult<BtcTradeOrderBook> GetOrderBook(string symbol, OrderSide side, CancellationToken ct = default) => GetOrderBookAsync(symbol, side, ct).Result;
        public async Task<WebCallResult<BtcTradeOrderBook>> GetOrderBookAsync(string symbol, OrderSide side, CancellationToken ct = default)
        {
            var result = await _baseClient.SendRequestInternalAsync<BtcTradeOrderBook>(_baseClient.GetUrl($"trades/{side.ToString().ToLower()}/{symbol}"), HttpMethod.Get, ct).ConfigureAwait(false);

            return result;
        }

    }
}
