using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.StockContext
{
    public class StockGetByCodeOrNameUseCase : UseCaseBase,
        IRequestHandler<string, StockOutput>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockGetByCodeOrNameUseCase(
            IRequestHandlerCollection useCases,
            IStockRepository stockRepository,
            IStockHistoryPriceRepository historyPriceRepository,
            IMapper mapper) : base(useCases)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        public async Task<StockOutput> ExecuteAsync(string? input)
        {
            var stock = await _stockRepository.GetByCodeOrByNameAsync(input);
            if(stock != null)
            {
                stock.UpdatePrice();
                var history = new StockHistoryPrice(stock.Price);
                stock.AddHistory(history);
                await _stockRepository.Update(stock);
                var output = _mapper.Map<StockOutput>(stock);
                return output;
            } else
            {
                return null;
            }
        }
    }
}
