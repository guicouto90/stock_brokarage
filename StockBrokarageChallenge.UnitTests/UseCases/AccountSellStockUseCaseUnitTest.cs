using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.AccountContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class AccountSellStockUseCaseUnitTest
    {
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IStockRepository> _stockRepository;

        public AccountSellStockUseCaseUnitTest()
        {
            _handler = new Mock<IRequestHandlerCollection>();
            _accountRepository = new Mock<IAccountRepository>();
            _stockRepository = new Mock<IStockRepository>();
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed_DueTo_ThereIsNoStockInThisWallet()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            var stockResult = new Stock("TestStock", "STCK4");
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuySellStocksInputs { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            var useCase = new AccountSellStockUseCase(_handler.Object, _accountRepository.Object, _stockRepository.Object);

            // Act and  Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await useCase.ExecuteAsync(input));
            Assert.Equal("You dont have this stock in your wallet", exception.Message);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Not_Suceed_DueTo_QuantityIsBiggerThanStockQuantity()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(10000.0);
            var stockResult = new Stock("TestStock", "STCK4");
            customerResult.Account.BuyStock(stockResult, 100);
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuySellStocksInputs { CustomerId = customerResult.Id, Quantity = 200, StockCode = "STCK4" };

            var useCase = new AccountSellStockUseCase(_handler.Object, _accountRepository.Object, _stockRepository.Object);

            // Act and  Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => await useCase.ExecuteAsync(input));
            Assert.Equal("Quantity must be lower than StockQuantity", exception.Message);
        }

        [Fact]
        public async Task AccountBuyStock_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(10000.0);
            var stockResult = new Stock("TestStock", "STCK4");
            customerResult.Account.BuyStock(stockResult, 100);
            _accountRepository.Setup(x => x.GetByCustomerIdWithWalletAsync(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));

            var input = new AccountBuySellStocksInputs { CustomerId = customerResult.Id, Quantity = 100, StockCode = "STCK4" };

            var useCase = new AccountSellStockUseCase(_handler.Object, _accountRepository.Object, _stockRepository.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal("Sold succeed - Quantity: 100, Stock STCK4", result);
        }
    }
}
