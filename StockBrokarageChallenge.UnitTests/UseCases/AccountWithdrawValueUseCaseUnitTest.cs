using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Inputs;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class AccountWithdrawValueUseCaseUnitTest
    {
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IAccountRepository> _accountRepository;

        public AccountWithdrawValueUseCaseUnitTest()
        {
            _handler = new Mock<IRequestHandlerCollection>();
            _accountRepository = new Mock<IAccountRepository>();
        }

        [Fact]
        public async Task AccountWithdrawValue_ExecuteAsyncMethod_Should_Not_Suceed_Due_To_No_Balance()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            var input = new AccountWithdrawDepositValueInput { CustomerId = customerResult.Id, Value = 100.0 };

            var useCase = new AccountWithdrawValueUseCase(_handler.Object, _accountRepository.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal("There is no enough balance to withdraw", result);
        }

        [Fact]
        public async Task AccountWithdrawValue_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            customerResult.Account.DepositValue(100);
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));
            var input = new AccountWithdrawDepositValueInput { CustomerId = customerResult.Id, Value = 100.0 };

            var useCase = new AccountWithdrawValueUseCase(_handler.Object, _accountRepository.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<string>(result);
            Assert.Equal(customerResult.Account.Balance, 0.0);
            Assert.Equal("Withdraw succeed", result);
        }
    }
}
