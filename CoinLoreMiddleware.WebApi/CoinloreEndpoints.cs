using CoinLoreMiddleware.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CoinLoreMiddleware.WebApi
{
    public static class CoinloreEndpoints
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/externalCrypto/fetchMarketInfo", async ([FromServices] ICryptoCurrencyService service) 
                => await service.GetFullInformation())
                .ShortCircuit();

            app.MapGet("/externalCrypto/fetchCoins", async ([FromQuery(Name="start")] string? start, [FromQuery(Name="limit")] string? limit, [FromServices] ICryptoCurrencyService service)
                => await service.GetTickersInformation(start, limit))
                  .ShortCircuit();

            app.MapGet("/externalCrypto/fetchCoin", async ([FromQuery(Name = "ids")] string ids, [FromServices] ICryptoCurrencyService service)
                => await service.GetTickerSpecific(ids))
                  .ShortCircuit();

            app.MapGet("/externalCrypto/fetchCoinMarket", async ([FromQuery(Name = "id")] string id, [FromServices] ICryptoCurrencyService service)
                => await service.First50CoinMarket(id))
                  .ShortCircuit();

            app.MapGet("/externalCrypto/fetchExchangeInfo", async ([FromServices] ICryptoCurrencyService service)
                => await service.GetAllExchanges())
                  .ShortCircuit();

            app.MapGet("/externalCrypto/fetchExchange", async ([FromQuery(Name = "id")] string id, [FromServices] ICryptoCurrencyService service)
                => await service.GetSingleExchange(id))
                  .ShortCircuit();

            app.MapGet("/externalCrypto/fetchSocialStats", async ([FromQuery(Name = "id")] string id, [FromServices] ICryptoCurrencyService service)
                => await service.GetSocialStats(id))
                  .ShortCircuit();
        }
    }
}
