using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinMarketCapProject.Models
{
    public class Status
    {
        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("error_message")]
        public object ErrorMessage { get; set; }
        [JsonProperty("elapsed")]
        public int Elapsed { get; set; }
        [JsonProperty("credit_count")]
        public int CreditCount { get; set; }
    }

    [JsonObject("USD")]
    public class USD
    {
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("volume_24h")]
        public double Volume24h { get; set; }
        [JsonProperty("percent_change_5m")]
        public double PercentChange5m { get; set; }
        [JsonProperty("percent_change_1h")]
        public double PercentChange1h { get; set; }
        [JsonProperty("percent_change_24h")]
        public double PercentChange24h { get; set; }
        [JsonProperty("percent_change_7d")]
        public double PercentChange7d { get; set; }
        [JsonProperty("market_cap")]
        public double MarketCap { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
    }

    public class Quote
    {
        [JsonProperty("USD")]
        public USD USD { get; set; }
    }

    public class Datum
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("circulating_supply")]
        public double CirculatingSupply { get; set; }
        [JsonProperty("total_supply")]
        public double TotalSupply { get; set; }
        [JsonProperty("max_supply")]
        public long? MaxSupply { get; set; }
        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }
        [JsonProperty("num_market_pairs")]
        public int NumMarketPairs { get; set; }
        [JsonProperty("cmc_rank")]
        public int CmcRank { get; set; }
        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }
        [JsonProperty("quote")]
        public Quote Quote { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }
}