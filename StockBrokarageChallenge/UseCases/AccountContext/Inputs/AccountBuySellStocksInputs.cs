using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs
{
    public class AccountBuySellStocksInputs
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        
        [Required]
        [StringLength(5, ErrorMessage = "Stock code must have 5 characters")]
        public string StockCode { get; set; }

        [JsonIgnore]
        public int CustomerId { get; set; }
    }
}
