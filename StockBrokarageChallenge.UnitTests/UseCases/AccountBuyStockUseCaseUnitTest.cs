using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.AccountContext;
using FluentAssertions;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class AccountBuyStockUseCaseUnitTest
    {
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IStockRepository> _stockRepository;

        public AccountBuyStockUseCaseUnitTest()
        {
            _handler = new Mock<IRequestHandlerCollection>();
            _accountRepository = new Mock<IAccountRepository>();
            _stockRepository = new Mock<IStockRepository>();
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed_Due_To_No_Balance()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            var stockResult = new Stock("TestStock", "STCK4");
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuySellStocksInputs { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            var useCase = new AccountBuyStockUseCase(_handler.Object, _accountRepository.Object, _stockRepository.Object);

            // Act and  Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await useCase.ExecuteAsync(input));
            Assert.Equal("There is no enough balance to buy these stocks", exception.Message);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(10000.0);
            var stockResult = new Stock("TestStock", "STCK4");
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuySellStocksInputs { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            var useCase = new AccountBuyStockUseCase(_handler.Object, _accountRepository.Object, _stockRepository.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal("Purchase succeed - Quantity: 100, Stock STCK4", result);
        }
    }
}
