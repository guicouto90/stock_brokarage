using StockBrokarageChallenge.Application.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs
{
    public class WalletOutput
    {
        public double TotalInvested { get; set; }

        public double CurrentBalance { get; set; }

        public List<StocksWallet> StocksWallet { get; set; }
    }
}
