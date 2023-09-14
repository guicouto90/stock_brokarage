using AutoFixture;
using AutoMapper;
using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.AccountContext;
using StockBrokarageChallenge.Application.UseCases.AccountContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.CustomerContext;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Outputs;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class AccountListTransactionHistoryUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IMapper> _mapper;

        public AccountListTransactionHistoryUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _handler = new Mock<IRequestHandlerCollection>();
            _accountRepository = new Mock<IAccountRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task AccountListTransactionsHistory_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var customerResult = new Customer("Robervaldo", "01234567890", 1, "1234567");
            _accountRepository.Setup(x => x.GetByCustomerId(It.IsAny<int>())).Returns(Task.FromResult(customerResult.Account));

           
            var expectedOutput = _fixture.Build<AccountOutput>()
                .With(x => x.AccountNumber, customerResult.Account.AccountNumber)
                .With(x => x.Balance, customerResult.Account.Balance)
                .With(x => x.Customer, customerResult.Account.Customer)
                .Create();
            _mapper.Setup(m => m.Map<AccountOutput>(It.IsAny<Account>())).Returns(expectedOutput);

            var useCase = new AccountListTransactionHistoryUseCase(_handler.Object, _accountRepository.Object, _mapper.Object);

            // Act
            var result = await useCase.ExecuteAsync(1);

            // Assert
            Assert.IsType<AccountOutput>(result);
        }
    }
}
