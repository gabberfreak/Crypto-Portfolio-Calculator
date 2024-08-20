namespace CoinLoreMiddleware.Services.Contracts
{
    public interface ICryptoCurrencyService
    {
        Task<string> GetFullInformation();
        Task<string> GetTickersInformation(string? start, string? limit);
        Task<string> GetTickerSpecific(string ids);
        Task<string> First50CoinMarket(string id);
        Task<string> GetAllExchanges();
        Task<string> GetSingleExchange(string id);
        Task<string> GetSocialStats(string id);
    }
}
