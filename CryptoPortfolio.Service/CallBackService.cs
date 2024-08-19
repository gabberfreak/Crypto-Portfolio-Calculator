using CryptoPortfolio.Domain.Models;
using CryptoPortfolio.Service.Contracts;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace CryptoPortfolio.Service
{
    public class CallBackService : ICallBackService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CallBackService(IHttpClientFactory httpClientFactory)
            =>_httpClientFactory = httpClientFactory;

        public async Task<AllCalculatorStatsModel?> CalculateStatsAsync(IEnumerable<CryptoWalletModel> wallet,MiddlewareSettings settings)
        {
            AllCalculatorStatsModel result = new AllCalculatorStatsModel();

            List<CoinPair> coinPercentageChanges = new List<CoinPair>();
            decimal initialPortfolioValue = 0m;
            decimal overallPortfolioChange = 0m;

            var client = _httpClientFactory.CreateClient("MiddlewareAPI");
            var url = settings.Coins;

            var callResult = await ExtractData(client, url);

            foreach (var item in wallet)
            {
                bool found = false;
                var start = 0;

                while (!found)
                {

                    if (callResult.Data.Any(x => x.Symbol == item.CryptoCurrencyName))
                    {
                        var coinData = callResult.Data.FirstOrDefault(x => x.Symbol == item.CryptoCurrencyName);
                        var marketPrice = decimal.Parse(coinData!.PriceUsd);

                        var percentageChange = ((marketPrice - item.CoinPrice) / item.CoinPrice) * 100;

                        coinPercentageChanges.Add(new CoinPair { CoinName = item.CryptoCurrencyName, PercentageChange = percentageChange.ToString(".##") + "%" });

                        overallPortfolioChange += (decimal)item.CoinsCount * marketPrice;

                        found = true;
                    }

                    if (!found)
                    {
                        var query = HttpUtility.ParseQueryString(string.Empty);
                        query["start"] = start.ToString();
                        query["limit"] = "100";

                        callResult = await ExtractData(client, url + "?" + query.ToString());

                        start += 100;
                    }
                }

                initialPortfolioValue += item.CoinPrice * (decimal)item.CoinsCount;
            }

            result.InitialValue = initialPortfolioValue.ToString(".##");
            result.CoinChange = coinPercentageChanges;
            result.Overall = overallPortfolioChange.ToString(".##");

            return result;

        }

        private async Task<GetCoinsResponseModel> ExtractData(HttpClient client, string? url) => 
            await client.GetFromJsonAsync<GetCoinsResponseModel>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}
