using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext.Inputs;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.StockContext
{
    public class StockCreateUseCase : UseCaseBase,
        IRequestHandler<StockCreateInput, StockOutput>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;
        public StockCreateUseCase(
            IRequestHandlerCollection useCases, 
            IStockRepository repository, 
            IMapper mapper) : base(useCases)
        {
            _stockRepository = repository;
            _mapper = mapper;
        }

        public async Task<StockOutput> ExecuteAsync(StockCreateInput input)
        {
            var isStockExist = await _stockRepository.GetByCodeOrByNameAsync(input.Code);
            if (isStockExist != null)
            {
                return null;
            }
            var stock = new Stock(input.Name, input.Code);
            var history = new StockHistoryPrice(stock.Price);
            stock.AddHistory(history);
            await _stockRepository.Create(stock);
            
            return _mapper.Map<StockOutput>(stock);
        }
    }
}