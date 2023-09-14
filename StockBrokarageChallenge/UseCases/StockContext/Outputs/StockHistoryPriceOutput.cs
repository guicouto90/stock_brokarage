namespace StockBrokarageChallenge.Application.UseCases.StockContext
{
    public class StockHistoryPriceOutput
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public double ActualPrice { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
