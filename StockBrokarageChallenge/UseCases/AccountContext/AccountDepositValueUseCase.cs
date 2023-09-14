using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountDepositValueUseCase : UseCaseBase,
        IRequestHandler<AccountWithdrawDepositValueInput, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        public AccountDepositValueUseCase(
            IRequestHandlerCollection useCases,
            IAccountRepository accountRepository,
            ITransactionHistoryRepository transactionHistoryRepository) : base(useCases)
        {
            _accountRepository = accountRepository;
            _transactionHistoryRepository = transactionHistoryRepository;
        }

        public async Task<string> ExecuteAsync(AccountWithdrawDepositValueInput? input)
        {
            var account = await _accountRepository.GetByNumberAsync(input.AccountNumber);
            if (account == null)
            {
                return null;
            }
            else if (account.CustomerId != input.CustomerId)
            {
                throw new HttpRequestException("Access denied to this account", null, System.Net.HttpStatusCode.Unauthorized);
            }
            try
            {
                account.DepositValue(input.Value);
                // await _transactionHistoryRepository.Create(account.LastTransaction());
                await _accountRepository.Update(account);
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
