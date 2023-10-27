using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountBuyStockUseCase : UseCaseBase,
        IRequestHandler<AccountBuySellStocksInputs, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStockRepository _stockRepository;

        public AccountBuyStockUseCase(
            IRequestHandlerCollection useCases, 
            IAccountRepository accountRepository, 
            IStockRepository stockRepository)
            : base(useCases)
        {
            _accountRepository = accountRepository;
            _stockRepository = stockRepository;
        }

        public async Task<string> ExecuteAsync(AccountBuySellStocksInputs? input)
        {
            try
            {
                var account = await _accountRepository.GetByCustomerIdWithWalletAsync(input.CustomerId).ConfigureAwait(false);
                var stock = await _stockRepository.GetByCodeOrByNameAsync(input.StockCode).ConfigureAwait(false);
                if(stock == null)
                {
                    throw new HttpRequestException("Stock Not Found", null, System.Net.HttpStatusCode.NotFound);
                } else
                {
                    account.BuyStock(stock, input.Quantity);
                    await _accountRepository.Update(account).ConfigureAwait(false);
                    return $"Purchase succeed - Quantity: {input.Quantity}, Stock {stock.Code}";
                }
            } catch(DomainExceptionValidation ex)
            {
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.BadRequest);
            } catch (Exception ex) 
            {
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
