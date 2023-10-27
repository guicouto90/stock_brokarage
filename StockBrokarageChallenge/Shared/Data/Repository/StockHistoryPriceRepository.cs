using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository
{
    public class StockHistoryPriceRepository
        : IStockHistoryPriceRepository
    {

        private readonly ApplicationDbContext _context;

        public StockHistoryPriceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StockHistoryPrice> Create(StockHistoryPrice entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<ICollection<StockHistoryPrice>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<StockHistoryPrice> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StockHistoryPrice> Remove(StockHistoryPrice entity)
        {
            throw new NotImplementedException();
        }

        public Task<StockHistoryPrice> Update(StockHistoryPrice entity)
        {
            throw new NotImplementedException();
        }
    }
}
