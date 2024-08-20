using CryptoPortfolio.Domain.Models;

namespace CryptoPortfolioCalculator.Web.Services
{
    public interface ICryptoMapperService
    {
        List<CryptoWalletModel> MapCryptoPortfoilioValues(string[] rows);
    }
}
