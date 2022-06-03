using BtcTrade.Net.Objects.Models;
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
    public class BtcTradeClientSpotApiAccount
    {
        private readonly BtcTradeClientSpotApi _baseClient;

        //private const string ApiTickenEndpoint = "auth";
        //private const string BalanceEndpoint = "balance";

        internal BtcTradeClientSpotApiAccount(BtcTradeClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        public WebCallResult<BtcTradeAuth> GetApiToken(CancellationToken ct = default) => GetApiTokenAsync(ct).Result;
        public async Task<WebCallResult<BtcTradeAuth>> GetApiTokenAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternalAsync<BtcTradeAuth>(_baseClient.GetUrl("auth"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }


        public WebCallResult<BtcTradeWallet> GetBalance(CancellationToken ct = default) => GetBalanceAsync(ct).Result;
        public async Task<WebCallResult<BtcTradeWallet>> GetBalanceAsync(CancellationToken ct = default)
        {
            return await _baseClient.SendRequestInternalAsync<BtcTradeWallet>(_baseClient.GetUrl("balance"), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
        }
    }
}
