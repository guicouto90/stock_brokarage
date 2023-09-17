using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.StockContext;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;

namespace StockBrokarageChallenge.Application.Shared.Mappers
{
    public class ResourceMapperProfile : Profile
    {
        public ResourceMapperProfile()
        {
            CreateMap<Stock, StockOutput>();
            CreateMap<StockHistoryPrice, StockHistoryPriceOutput>();
            CreateMap<Customer, CustomerOutput>();
            CreateMap<Account, AccountOutput>();
            CreateMap<TransactionHistory, TransactionHistoryOutput>();
            CreateMap<Wallet, WalletOutput>();
            CreateMap<StocksWallet, StocksWalletOutput>();
        }
    }
}
