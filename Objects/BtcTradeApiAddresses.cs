namespace BtcTrade.Net.Objects
{
    public class BtcTradeApiAddresses
    {
        public string RestClientAddress { get; set; } = "";

        public static BtcTradeApiAddresses Default = new BtcTradeApiAddresses
        {
            RestClientAddress = "https://btc-trade.com.ua/api",
        };
    }
}
