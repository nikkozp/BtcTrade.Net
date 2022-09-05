using BtcTrade.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace BtcTrade.Net
{
    public class BtcTradeAuthenticationProvider : AuthenticationProvider
    {
        public static string Token = "";


        private readonly SHA256Managed encryptor;
        private readonly INonceProvider _nonceProvider;

        public BtcTradeAuthenticationProvider(ApiCredentials credentials, INonceProvider? nonceProvider) : base(credentials)
        {
            encryptor = new SHA256Managed();

            _nonceProvider = nonceProvider ?? new BtcTradeNonceProvider();
        }

        public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
        {
            uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
            headers = new Dictionary<string, string>();

            if (!auth)
                return;

            var parameters = parameterPosition == HttpMethodParameterPosition.InUri ? uriParameters : bodyParameters;

            var nonce = _nonceProvider.GetNonce();

            parameters.Add("out_order_id", new Random().Next(1000));
            parameters.Add("nonce", nonce);
            parameters = new SortedDictionary<string, object>(parameters.ToDictionary(p => p.Key, p => p.Value).OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));

            if (String.IsNullOrEmpty(Token) || uri.PathAndQuery.EndsWith("auth"))
            {
                headers.Add("public_key", Credentials.Key.GetString());

                var pramsString = parameters.ToDictionary(p => p.Key, p => p.Value).CreateParamString(true, ArrayParametersSerialization.Array);

                headers.Add("api_sign", BytesToHexString(encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{pramsString}{Credentials.Secret.GetString()}"))).ToLower());
            }
            else
                headers.Add("token", Token);
        }

        //public override Dictionary<string, object> AddAuthenticationToParameters(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, HttpMethodParameterPosition parameterPosition, ArrayParametersSerialization arraySerialization)
        //{
        //    if (!signed)
        //        return parameters;

        //    parameters.Add("out_order_id", new Random().Next(1000));
        //    parameters.Add("nonce", Nonce);

        //    parameters = parameters.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);

        //    return parameters;
        //}

        //public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, HttpMethodParameterPosition parameterPosition, ArrayParametersSerialization arraySerialization)
        //{
        //    if (!signed)
        //        return new Dictionary<string, string>();

        //    var result = new Dictionary<string, string>();

        //    if (String.IsNullOrEmpty(Token))
        //    {
        //        result.Add("public_key", Credentials.Key.GetString());
        //        result.Add("api_sign", ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{parameters.CreateParamString(true, ArrayParametersSerialization.Array)}{Credentials.Secret.GetString()}"))).ToLower());
        //    }
        //    else
        //        result.Add("token", Token);


        //    return result;
        //}
    }
}
