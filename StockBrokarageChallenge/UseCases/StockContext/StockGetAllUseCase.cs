using AutoMapper;
using StockBrokarageChallenge.Application.Shared.Abstractions;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using StockBrokarageChallenge.Application.UseCases.StockContext.Outputs;

namespace StockBrokarageChallenge.Application.UseCases.StockContext
{
    public class StockGetAllUseCase : UseCaseBase,
        IRequestHandler<object, ICollection<StockOutput>>
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockHistoryPriceRepository _historyPriceRepository;
        private readonly IMapper _mapper;
        public StockGetAllUseCase(
            IRequestHandlerCollection useCases, 
            IStockRepository repository, 
            IMapper mapper,
            IStockHistoryPriceRepository stockHistoryPriceRepository
            ) : base(useCases)
        {
            _stockRepository = repository;
            _mapper = mapper;
            _historyPriceRepository = stockHistoryPriceRepository;
        }

        public async Task<ICollection<StockOutput>> ExecuteAsync(object? input)
        {

            var response = await _stockRepository.GetAll().ConfigureAwait(false);
            var listResponse = new List<StockOutput>();
            foreach (var item in response)
            {
                item.UpdatePrice();
                
                var history = new StockHistoryPrice(item.Price);
                item.AddHistory(history);
                await _historyPriceRepository.Create(history).ConfigureAwait(false);
                await _stockRepository.Update(item).ConfigureAwait(false);
                
                listResponse.Add(_mapper.Map<StockOutput>(item));
            }
            return listResponse;
        }
    }
}
