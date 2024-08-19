using CryptoPortfolioCalculator.Web.Services;
using CryptoPortfolio.Service.Contracts;
using CryptoPortfolio.Service;
using CryptoPortfolio.Domain.Models;

namespace CryptoPortfolioCalculator.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<ICallBackService, CallBackService>("MiddlewareAPI", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://localhost:7254/externalCrypto/");
            });

            var configSection = builder.Configuration.GetSection("MiddlewareEndPoints");
            builder.Services.Configure<MiddlewareSettings>(configSection);

            builder.Services.AddTransient<ICryptoMapperService,  CryptoMapperService>();
            builder.Services.AddTransient<ICallBackService, CallBackService>();

            var app = builder.Build();

            var loggerFactory = app.Services.GetService<ILoggerFactory>();
            loggerFactory.AddFile(builder.Configuration["Logging:LogPath"].ToString());

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            

            app.Run();
        }
    }
}
