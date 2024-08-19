namespace CryptoPortfolio.Domain.Models
{
    public class CryptoWalletModel
    {
        public CryptoWalletModel(double coinsCount, string cryptoCurrencyName, decimal coinPrice)
        {
            CoinsCount = coinsCount;
            CryptoCurrencyName = cryptoCurrencyName;
            CoinPrice = coinPrice;
        }

        public double CoinsCount { get; set; }

        public string CryptoCurrencyName { get; set; }

        public decimal CoinPrice { get; set; }
    }
}
