using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountListTransactionHistoryUseCase : UseCaseBase,
        IRequestHandler<int, AccountOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountListTransactionHistoryUseCase(
            IRequestHandlerCollection useCases,
            IAccountRepository accountRepository, IMapper mapper) : base(useCases)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<AccountOutput?> ExecuteAsync(int input)
        {
            var account = await _accountRepository.GetByCustomerId(input).ConfigureAwait(false);
            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountOutput>(account);
        }
    }
}
