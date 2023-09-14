using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;

namespace StockBrokarageChallenge.Application.UseCases.AccountContext
{
    public class AccountSellStockUseCase : UseCaseBase,
        IRequestHandler<AccountBuySellStocksInputs, string>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStockRepository _stockRepository;

        public AccountSellStockUseCase(
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
                var account = await _accountRepository.GetByCustomerIdWithWalletAsync(input.CustomerId);
                var stock = await _stockRepository.GetByCodeOrByNameAsync(input.StockCode);
                if (stock == null)
                {
                    throw new HttpRequestException("Stock Not Found", null, System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    account.SellStock(stock, input.Quantity);
                    await _accountRepository.Update(account);
                    return $"Sold succeed - Quantity: {input.Quantity}, Stock {stock.Code}";
                }
            }
            catch (DomainExceptionValidation ex)
            {
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message, ex, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
