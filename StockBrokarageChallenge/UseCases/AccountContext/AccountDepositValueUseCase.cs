using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountDepositValueUseCase : UseCaseBase,
        IRequestHandler<AccountWithdrawDepositValueInput, string>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountDepositValueUseCase(
            IRequestHandlerCollection useCases,
            IAccountRepository accountRepository) : base(useCases)
        {
            _accountRepository = accountRepository;
        }

        public async Task<string> ExecuteAsync(AccountWithdrawDepositValueInput? input)
        {
            var account = await _accountRepository.GetByCustomerId(input.CustomerId).ConfigureAwait(false);
            if (account == null)
            {
                return null;
            }
            try
            {
                account.DepositValue(input.Value);
                await _accountRepository.Update(account).ConfigureAwait(false);
                return "Deposit succeed";
            }
            catch (DomainExceptionValidation ex)
            {
                return ex.Message;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
