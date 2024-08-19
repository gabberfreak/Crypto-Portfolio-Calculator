using CryptoPortfolio.Domain.Models;

namespace CryptoPortfolio.Service.Contracts
{
    public interface ICallBackService
    {
        Task<AllCalculatorStatsModel?> CalculateStatsAsync(IEnumerable<CryptoWalletModel> wallet, MiddlewareSettings settings);
    }
}
