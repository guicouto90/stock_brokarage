using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountListAccountUseCase : UseCaseBase,
        IRequestHandler<int, WalletOutput>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public AccountListAccountUseCase(
            IRequestHandlerCollection useCases,
            IAccountRepository accountRepository,
            IMapper mapper) : base(useCases)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<WalletOutput> ExecuteAsync(int input)
        {
            var account = await _accountRepository.GetByCustomerIdWithWalletAsync(input).ConfigureAwait(false);

            account.Wallet.UpdateCurrentBalance();
            await _accountRepository.Update(account).ConfigureAwait(false);

            return _mapper.Map<WalletOutput>(account.Wallet);
        }
    }
}
