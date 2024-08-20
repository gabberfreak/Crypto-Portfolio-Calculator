using CryptoPortfolio.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace CryptoPortfolioCalculator.Web.Hubs
{
    public class CalculatePortfolioPercentChangeHub : Hub
    {
        public async Task UpdatePortfolioValues(AllCalculatorStatsModel model)
        {
            await Clients.All.SendAsync("AppendValues", model);
        }

    }
}
