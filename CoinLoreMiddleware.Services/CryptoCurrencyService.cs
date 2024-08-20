using CoinLoreMiddleware.Services.Contracts;
using CoinLoreMiddleware.Services.Helpers;

namespace CoinLoreMiddleware.Services
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CryptoCurrencyService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;


        public async Task<string> GetFullInformation()
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:GlobalData")?.Value;

            var response = await httpClient.GetAsync($"{url}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTickersInformation(string? start, string? limit)
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:Tickers")?.Value;

            var builder = new UriBuilderHelper(httpClient.BaseAddress + url);

            if (!string.IsNullOrWhiteSpace(start))
                builder.AddParameter(nameof(start), start);

            if (!string.IsNullOrWhiteSpace(limit))
                builder.AddParameter(nameof(limit), limit);

            var response = await httpClient.GetAsync($"{builder.GetUri()}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTickerSpecific(string ids)
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:TickerById")?.Value;

            var response = await httpClient.GetAsync($"{url?.Replace("{0}", ids)}");

            return await response.Content.ReadAsStringAsync();

        }

        public async Task<string> First50CoinMarket(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:CoinMarket")?.Value;

            var response = await httpClient.GetAsync($"{url?.Replace("{0}", id)}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAllExchanges()
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:AllExchanges")?.Value;

            var response = await httpClient.GetAsync($"{url}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSingleExchange(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:SingleExchange")?.Value;

            var response = await httpClient.GetAsync($"{url?.Replace("{0}", id)}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetSocialStats(string id)
        {
            var httpClient = _httpClientFactory.CreateClient("Coinlore");
            var url = ConfigurationHelper.config?.GetSection("CryptoApiUrls:SocialStats")?.Value;

            var response = await httpClient.GetAsync($"{url?.Replace("{0}", id)}");

            return await response.Content.ReadAsStringAsync();
        }
    }
}
