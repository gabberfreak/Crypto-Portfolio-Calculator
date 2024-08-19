using CryptoPortfolio.Domain.Models;

namespace CryptoPortfolioCalculator.Web.Models
{
    public class CalculatorViewModel
    {
        public string InitialValue { get; set; }

        public List<CoinPair> CoinChange { get; set; }

        public string Overall { get; set; }
    }

   
}
