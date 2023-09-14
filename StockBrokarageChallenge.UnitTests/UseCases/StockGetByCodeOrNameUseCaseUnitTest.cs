using AutoFixture;
using AutoMapper;
using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.StockContext;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class StockGetByCodeOrNameUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<IStockHistoryPriceRepository> _stockHistoryPriceRepository;
        private readonly Mock<IMapper> _mapper;

        public StockGetByCodeOrNameUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _handler = new Mock<IRequestHandlerCollection>();
            _stockRepository = new Mock<IStockRepository>();
            _stockHistoryPriceRepository = new Mock<IStockHistoryPriceRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ListStockByCodeOrName_ExecuteAsyncMethod_Succeed()
        {
            // Arrange
            Stock stockResult = new Stock("Vale", "VALE4");
            var input = "VALE4"; 

            var stockHistoryPriceResult = _fixture.Create<StockHistoryPrice>();
            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(stockResult));
            var expectedOutput = _fixture.Build<StockOutput>().With(x => x.Name, stockResult.Name).With(x => x.Code, stockResult.Code).Create();
            _mapper.Setup(m => m.Map<StockOutput>(It.IsAny<Stock>())).Returns(expectedOutput);

            var useCase = new StockGetByCodeOrNameUseCase(_handler.Object, _stockRepository.Object, _stockHistoryPriceRepository.Object, _mapper.Object);


            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<StockOutput>(result);
            Assert.Equal(stockResult.Name, result.Name);
            Assert.Equal(stockResult.Code, result.Code);
        }

        [Fact]
        public async Task ListStockByCodeOrName_ExecuteAsyncMethod_NotFound_ReturnNull()
        {
            // Arrange
            var input = "VALE5";

            _stockRepository.Setup(x => x.GetByCodeOrByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<Stock>(null));
            var useCase = new StockGetByCodeOrNameUseCase(_handler.Object, _stockRepository.Object, _stockHistoryPriceRepository.Object, _mapper.Object);

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.Null(result);
        }
    }
}
