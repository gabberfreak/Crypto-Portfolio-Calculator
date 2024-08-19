using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CryptoPortfolio.Domain.Models
{
    public class GetCoinsResponseModel
    {
        [JsonPropertyName("data")]
        public IEnumerable<Data> Data { get; set; }

        [JsonPropertyName("info")]
        public Info Info { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nameid")]
        public string NameId { get; set; }

        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("price_usd")]
        public string PriceUsd { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public string PercentChange24h { get; set; }

        [JsonPropertyName("percent_change_1h")]
        public string PercentChange1h { get; set; }

        [JsonPropertyName("percent_change_7d")]
        public string PercentChange7d { get; set; }

        [JsonPropertyName("price_btc")]
        public string PriceBtc { get; set; }

        [JsonPropertyName("market_cap_usd")]
        public string MarketCapUsd { get; set; }

        [JsonPropertyName("volume24")]
        public decimal Volume24 { get; set; }

        [JsonPropertyName("volume24a")]
        public decimal Volume24a { get; set; }

        [JsonPropertyName("csupply")]
        public string CSupply { get; set; }

        [JsonPropertyName("tsupply")]
        public string TSupply { get; set; }

        [JsonPropertyName("msupply")]
        public string MSupply { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("coins_num")]
        public int CoinsNum { get; set; }

        [Timestamp]
        [JsonPropertyName("time")]
        public int Time { get; set; }
    }
}
