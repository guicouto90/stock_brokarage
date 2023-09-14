using AutoFixture;
using AutoMapper;
using Moq;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;
using StockBrokarageChallenge.Application.UseCases.StockContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockBrokarageChallenge.UnitTests.UseCases
{
    public class StockGetAllUseCaseUnitTest
    {
        private readonly Fixture _fixture;
        private readonly Mock<IRequestHandlerCollection> _handler;
        private readonly Mock<IStockRepository> _stockRepository;
        private readonly Mock<IStockHistoryPriceRepository> _stockHistoryPriceRepository;
        private readonly Mock<IMapper> _mapper;

        public StockGetAllUseCaseUnitTest()
        {
            _fixture = new Fixture();
            _handler = new Mock<IRequestHandlerCollection>();
            _stockRepository = new Mock<IStockRepository>();
            _stockHistoryPriceRepository = new Mock<IStockHistoryPriceRepository>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ListAllStocks_ExecuteAsyncMethod_Succeed()
        {
            // Arrange
            ICollection<Stock> stocksResult = new List<Stock>
            {
                { new Stock("Vale", "VALE4") },
                new Stock("Cielo", "CIEL4")
            };
            var stockHistoryPriceResult = _fixture.Create<StockHistoryPrice>();
            _stockRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(stocksResult));
            _mapper.Setup(m => m.Map<StockOutput>(It.IsAny<Stock>())).Returns(_fixture.Create<StockOutput>());

            var useCase = new StockGetAllUseCase(_handler.Object, _stockRepository.Object, _mapper.Object, _stockHistoryPriceRepository.Object);


            // Act
            var result = await useCase.ExecuteAsync(null);

            // Assert
            Assert.IsType<List<StockOutput>>(result);
        }
    }
}
