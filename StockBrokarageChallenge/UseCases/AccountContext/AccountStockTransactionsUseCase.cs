using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.Shared.Models.Enums;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountStockTransactionsUseCase : UseCaseBase,
        IRequestHandler<int, List<TransactionHistoryOutput>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountStockTransactionsUseCase(
            IRequestHandlerCollection useCases, 
            IAccountRepository accountRepository, 
            IMapper mapper) : base(useCases)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<List<TransactionHistoryOutput>> ExecuteAsync(int input)
        {
            var account = await _accountRepository.GetByCustomerId(input).ConfigureAwait(false);

            var stockTransactionHistory = account.TransactionHistories
                .Where(th => th.TypeTransaction == TypeTransaction.BUY_STOCK
                || th.TypeTransaction == TypeTransaction.SELL_STOCK).ToList();

            var output = new List<TransactionHistoryOutput>();
            foreach(var transaction in stockTransactionHistory) 
            { 
                output.Add(_mapper.Map<TransactionHistoryOutput>(transaction));
            }

            return output;
        }
    }
}
