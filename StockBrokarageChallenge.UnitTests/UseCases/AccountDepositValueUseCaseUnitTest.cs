using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.AccountContext;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class AccountDepositValueUseCaseUnitTest
    {
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IAccountRepository> _accountRepository;

        public AccountDepositValueUseCaseUnitTest()
        {
            _handler = new Mock<IRequestHandlerCollection>();
            _accountRepository = new Mock<IAccountRepository>();
        }

        [Fact]
        public async Task AccountDepositValue_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            var input = new AccountWithdrawDepositValueInput { CustomerId = customerResult.Id, Value = 100.0 };

            var useCase = new AccountDepositValueUseCase(_handler.Object, _accountRepository.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal(customerResult.Account.Balance, 100.0);
            Assert.Equal("Deposit succeed", result);
        }
    }
}
