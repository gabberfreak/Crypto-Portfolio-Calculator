namespace CryptoPortfolio.Domain.Models
{
    public class AllCalculatorStatsModel
    {
        public string InitialValue { get; set; }

        public List<CoinPair> CoinChange { get; set; }

        public string Overall { get; set; }
    }
}
