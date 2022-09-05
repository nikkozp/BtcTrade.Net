using BtcTrade.Net.Enums;
using BtcTrade.Net.Objects;
using BtcTrade.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
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
    public class BtcTradeClientSpotApiTrading
    {
        private readonly BtcTradeClientSpotApi _baseClient;

        //private const string PlaceOrderEndpoint = "{}/{}";
        //private const string QueryOrderEndpoint = "order/status/{}";
        //private const string CancelOrderEndpoint = "remove/order/{}";
        //private const string OpenOrdersEndpoint = "my_orders/{}";
        //private const string MoveOrderEndpoint = "move/order/{}/{}";

        internal BtcTradeClientSpotApiTrading(BtcTradeClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        public WebCallResult<BtcTradeOpenOrders> GetOpenOrders(string symbol, CancellationToken ct = default) => GetOpenOrdersAsync(symbol, ct).Result;
        public async Task<WebCallResult<BtcTradeOpenOrders>> GetOpenOrdersAsync(string symbol, CancellationToken ct = default)
        {

            return await _baseClient.SendRequestInternalAsync<BtcTradeOpenOrders>(_baseClient.GetUrl($"/my_orders/{symbol}"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        public WebCallResult<BtcTradePlaceOrder> PlaceOrder(string symbol, OrderSide side, decimal price, decimal quantity, CancellationToken ct = default) => PlaceOrderAsync(symbol, side, price, quantity, ct).Result;
        public async Task<WebCallResult<BtcTradePlaceOrder>> PlaceOrderAsync(string symbol, OrderSide side, decimal price, decimal quantity, CancellationToken ct = default)
        {

            var parameters = new Dictionary<string, object>
            {
                { "count", quantity.ToString().Replace(",",".") },
                { "price",  price.ToString().Replace(",",".")}
            };

            return await _baseClient.SendRequestInternalAsync<BtcTradePlaceOrder>(_baseClient.GetUrl($"/{side.ToString().ToLower()}/{symbol}"), HttpMethod.Post, ct, parameters, true).ConfigureAwait(false);
        }

        public WebCallResult<BtcTradePlaceOrder> MoveOrder(long orderId, decimal price, CancellationToken ct = default) => MoveOrderAsync(orderId, price, ct).Result;
        public async Task<WebCallResult<BtcTradePlaceOrder>> MoveOrderAsync(long orderId, decimal price, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternalAsync<BtcTradePlaceOrder>(_baseClient.GetUrl($"/move/order/{orderId}/{price.ToString().Replace(",", ".")}"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        public WebCallResult<BtcTradeQueryOrder> GetOrder(long orderId, CancellationToken ct = default) => GetOrderAsync(orderId, ct).Result;
        public async Task<WebCallResult<BtcTradeQueryOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternalAsync<BtcTradeQueryOrder>(_baseClient.GetUrl($"/order/status/{orderId}"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

        public WebCallResult<BtcTradeCancelOrder> CancelOrder(long orderId, CancellationToken ct = default) => CancelOrderAsync(orderId, ct).Result;
        public async Task<WebCallResult<BtcTradeCancelOrder>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternalAsync<BtcTradeCancelOrder>(_baseClient.GetUrl($"/remove/order/{orderId}"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }

    }
}
