using StockBrokarageChallenge.Application.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs
{
    public class AccountOutput
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public int AccountNumber { get; set; }
        public double Balance { get; set; }

        public ICollection<TransactionHistoryOutput> TransactionHistories { get; set; }

    }
}
