using AutoFixture;
using AutoMapper;
using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext;
using StockBrokarageChallenge.Application.UseCases.StockContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;


namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class StockCreateUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<IStockHistoryPriceRepository> _stockHistoryPriceRepository;
        private readonly Mock<IMapper> _mapper;

        public StockCreateUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _handler = new Mock<IRequestHandlerCollection>();
            _stockRepository = new Mock<IStockRepository>();
            _stockHistoryPriceRepository = new Mock<IStockHistoryPriceRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task StockCreateUseCase_Creation_Should_Suceed()
        {
            var useCase = new StockCreateUseCase(_handler.Object, _stockRepository.Object, _mapper.Object);
           

            Assert.NotNull(useCase);
        }

        [Fact]
        public async Task StockCreateUseCase_ExecuteAsyncMethod_Should_Suceed()
        {
            // Arrange
            var input = _fixture.Build<StockCreateInput>().With(x => x.Name, "Vale").With(x => x.Code, "VALE4").Create();
            var stockResult = new Stock(input.Name, input.Code);

            var stockHistoryPriceResult = new StockHistoryPrice(stockResult.Price);
            stockResult.AddHistory(stockHistoryPriceResult);
            _stockRepository.Setup(x => x.Create(It.IsAny<Stock>())).Returns(Task.FromResult(stockResult));
            _stockHistoryPriceRepository.Setup(x => x.Create(It.IsAny<StockHistoryPrice>())).Returns(Task.FromResult(stockHistoryPriceResult));

            var expectedOutput = _fixture.Build<StockOutput>().With(x => x.Name, stockResult.Name).With(x => x.Code, stockResult.Code).With(x => x.Price, stockResult.Price).Create();
            _mapper.Setup(m => m.Map<StockOutput>(It.IsAny<Stock>())).Returns(expectedOutput);

            var useCase = new StockCreateUseCase(_handler.Object, _stockRepository.Object, _mapper.Object);
            

            // Act
            var result = await useCase.ExecuteAsync(input);

            // Assert
            Assert.IsType<StockOutput>(result);
            Assert.Equal(stockResult.Name, result.Name);
            Assert.Equal(stockResult.Code, result.Code);
            Assert.Equal(stockResult.Price, result.Price);
        }
    }
}
