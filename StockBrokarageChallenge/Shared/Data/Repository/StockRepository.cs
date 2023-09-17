using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository
{
    public sealed class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> Create(Stock entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            var newStock = await _context.Stocks.Where(e => e.Name == entity.Name).FirstOrDefaultAsync();
            return newStock;
        }

        public async Task<ICollection<Stock>> GetAll()
        {
            // Include is to get related entities
            return await _context.Stocks.Include(stock => stock.History).ToListAsync();
        }

        public async Task<Stock> GetByCodeOrByNameAsync(string input)
        {
            if (input.Length == 5)
            {
                var stock = await _context.Stocks.Where(e => e.Code == input).Include(s => s.History).FirstOrDefaultAsync();
                return stock;
            }
            else
            {
                var stock = await _context.Stocks.Where(e => e.Name == input).Include(s => s.History).FirstOrDefaultAsync();
                return stock;
            }
        }

        public async Task<Stock> GetById(int id)
        {
            var stock = await _context.Stocks.Where(e => e.Id == id).FirstOrDefaultAsync();
            return stock;
        }

        public async Task<Stock> Remove(Stock entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Stock> Update(Stock entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
