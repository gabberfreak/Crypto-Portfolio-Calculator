using CoinLoreMiddleware.Services.Contracts;
using CoinLoreMiddleware.Services.Helpers;
using CoinLoreMiddleware.Services;

namespace CoinLoreMiddleware.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpClient("Coinlore", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.coinlore.net/api/");
            });

            builder.Services.AddSingleton<ICryptoCurrencyService, CryptoCurrencyService>();

            ConfigurationHelper.Initialize(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            CoinloreEndpoints.Map(app);

            app.Run();
        }
    }
}
