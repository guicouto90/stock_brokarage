using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces
{
    public interface IStockRepository : IRepository<Stock>
    { 
        Task<Stock> GetByCodeOrByNameAsync(string input);
    }
}
