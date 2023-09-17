using StockBrokarageChallenge.Application.Shared.Models.Enums;
using System.Text.Json.Serialization;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs
{
    public class TransactionHistoryOutput
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TypeTransaction TypeTransaction { get; set; }

        public double TransactionValue { get; set; }

        public string? StockCode { get; set; }

        public int? StockQuantity { get; set; }

        public double? StockPrice { get; set; }

        public DateTime Date { get; set; }

    }
}
