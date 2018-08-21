using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using CoinMarketCapProject.Models;
using Newtonsoft.Json;

namespace CoinMarketCapProject
{
    public class HttpHlp
    {
        public RootObject GetCoins()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");
            webRequest.Method = "GET";
            webRequest.Headers.Add("X-CMC_PRO_API_KEY", "f23cc791-6731-4289-8ce4-cba56da27962");
            //rq.Headers.Add("start", "1");
            //rq.Headers.Add("limit", "30"); //not works?
            webRequest.Headers.Add("sort", "market_cap"); // default is DESC

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            RootObject coins;
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                string result = sr.ReadToEnd();
                coins = JsonConvert.DeserializeObject<RootObject>(result);
            }

            // calculate percent for every 5 minutes
            Dictionary<int, USD> lastData;
            if (HttpContext.Current.Session["LastData"] != null)
            {
                bool changed = false;
                lastData = (Dictionary<int, USD>)HttpContext.Current.Session["LastData"];
                foreach (var coin in coins.Data)
                {
                    var currUSD = lastData[coin.Id];
                    if ((coin.Quote.USD.LastUpdated - currUSD.LastUpdated).TotalMinutes >= 5)
                    {
                        coin.Quote.USD.PercentChange5m = ((coin.Quote.USD.Price - currUSD.Price) * 100) / currUSD.Price;
                        lastData[coin.Id] = coin.Quote.USD;
                        changed = true;
                    }
                    else
                    {
                        coin.Quote.USD.PercentChange5m = currUSD.PercentChange5m;
                    }
                }
                if (changed)
                {
                    HttpContext.Current.Session["LastData"] = lastData;
                }
            }
            else
            {
                lastData = new Dictionary<int, USD>();
                foreach (var coin in coins.Data)
                {
                    lastData.Add(coin.Id, coin.Quote.USD);
                }
                HttpContext.Current.Session["LastData"] = lastData;
            }
            return coins;
        }
    }
}