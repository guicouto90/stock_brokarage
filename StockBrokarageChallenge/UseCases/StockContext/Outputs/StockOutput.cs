using StockBrokarageChallenge.Application.Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace StockBrokarageChallenge.Application.UseCases.StockContext.Outputs
{
    public class StockOutput {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
        public double Price { get; set; }
        public ICollection<StockHistoryPriceOutput> History { get; set; }
    }
}
