using CryptoPortfolio.Domain.Models;

namespace CryptoPortfolioCalculator.Web.Services
{
    public class CryptoMapperService : ICryptoMapperService
    {
        public List<CryptoWalletModel> MapCryptoPortfoilioValues(string[] rows)
        {
            var walletBuilder = new List<CryptoWalletModel>();

            if (rows.Length == 0)
            {
                return null;
            }

            foreach (var line in rows)
            {
                var currentLine = line.Split('|', StringSplitOptions.RemoveEmptyEntries);

                var coinsCount = double.TryParse(currentLine[0], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double parsedCoinsCount);
                var cryptoCurrName = currentLine[1];
                var initialCoinPrice = decimal.TryParse(currentLine[2], System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out decimal parsedCoinPrice);

                walletBuilder.Add(new CryptoWalletModel(parsedCoinsCount, cryptoCurrName, parsedCoinPrice));
            }

            return walletBuilder;
        }
    }
}
