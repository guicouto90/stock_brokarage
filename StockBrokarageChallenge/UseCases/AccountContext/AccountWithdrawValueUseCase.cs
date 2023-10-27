using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountWithdrawValueUseCase : UseCaseBase,
        IRequestHandler<AccountWithdrawDepositValueInput, string>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountWithdrawValueUseCase(
            IRequestHandlerCollection useCases,
            IAccountRepository accountRepository) : base(useCases)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string?> ExecuteAsync(AccountWithdrawDepositValueInput? input)
        {
            var account = await _accountRepository.GetByCustomerId(input.CustomerId).ConfigureAwait(false);
            if (account == null)
            {
                return null;
            }
            try
            {
                account.WithdrawValue(input.Value);
                await _accountRepository.Update(account).ConfigureAwait(false);
                return "Withdraw succeed";
            } catch (DomainExceptionValidation ex)
            {
                return ex.Message;
            } catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
