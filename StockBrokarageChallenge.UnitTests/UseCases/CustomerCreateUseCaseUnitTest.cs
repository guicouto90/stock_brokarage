using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.StockContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.UseCases.CustomerContext;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Inputs;
using StockBrokarageChallenge.Application.Shared.Data.Repository;
using StockBrokarageChallenge.Application.UseCases.CustomerContext.Outputs;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class CustomerCreateUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IMapper> _mapper;

        public CustomerCreateUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _handler = new Mock<IRequestHandlerCollection>();
            _customerRepository = new Mock<ICustomerRepository>();
            _accountRepository = new Mock<IAccountRepository>();
            _mapper = new Mock<IMapper>();
        }


        [Fact]
        public void CustomerCreateUseCase_Creation_Should_Suceed()
        {
            var useCase = new CustomerCreateUseCase(_handler.Object, _customerRepository.Object, _accountRepository.Object, _mapper.Object);


            Assert.NotNull(useCase);
        }

        [Fact]
        public async Task CustomerCreateUseCase_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var input = _fixture.Build<CustomerCreateInput>()
                .With(x => x.Name, "Robervaldo")
                .With(x => x.Cpf, "12345678910")
                .With(x => x.Password, "1234567")
                .Create();
            var customerResult = new Customer(input.Name, input.Cpf, 1, input.Password);

            _customerRepository.Setup(x => x.Create(It.IsAny<Customer>())).Returns(Task.FromResult(customerResult));
            _accountRepository.Setup(x => x.Create(It.IsAny<Account>())).Returns(Task.FromResult(customerResult.Account));
            
            var expectedOutput = _fixture.Build<CustomerOutput>()
                .With(x => x.Name, customerResult.Name)
                .With(x => x.Cpf, customerResult.Cpf)
                .Create();
            _mapper.Setup(m => m.Map<CustomerOutput>(It.IsAny<Customer>())).Returns(expectedOutput);

            var useCase = new CustomerCreateUseCase(_handler.Object, _customerRepository.Object, _accountRepository.Object, _mapper.Object);


            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<CustomerOutput>(result);
            Assert.Equal(customerResult.Name, result.Name);
            Assert.Equal(customerResult.Cpf, result.Cpf);
            Assert.True(customerResult.Account.VerifyPassword(input.Password));
        }

        [Fact]
        public async Task CustomerCreateUseCase_ExecuteAsyncMethod_Should_ReturnNull()
        {
            // Arrange
            var input = _fixture.Build<CustomerCreateInput>()
                .With(x => x.Name, "Robervaldo")
                .With(x => x.Cpf, "01234567890")
                .With(x => x.Password, "1234567")
                .Create();
            var customerResult = new Customer(input.Name, input.Cpf, 1, input.Password);

            _customerRepository.Setup(x => x.GetByCpfAsync(It.IsAny<string>())).Returns(Task.FromResult(customerResult));
            var useCase = new CustomerCreateUseCase(_handler.Object, _customerRepository.Object, _accountRepository.Object, _mapper.Object);


            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.Null(result);
        }
    }
}
