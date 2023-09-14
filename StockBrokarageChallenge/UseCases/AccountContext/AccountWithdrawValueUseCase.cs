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

        public async Task<string> ExecuteAsync(AccountWithdrawDepositValueInput? input)
        {
            var account = await _accountRepository.GetByNumberWithTransactionHistoryAsync(input.AccountNumber);
            if (account == null)
            {
                return null;
            } else if(account.CustomerId != input.CustomerId)
            {
                throw new HttpRequestException("Access denied to this account", null, System.Net.HttpStatusCode.Unauthorized);
            }
            try
            {
                account.WithdrawValue(input.Value);
                await _accountRepository.Update(account);
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
