using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;
using System.Diagnostics.CodeAnalysis;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository
{
    [ExcludeFromCodeCoverage]
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
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<ICollection<Stock>> GetAll()
        {
            // Include is to get related entities
            return await _context.Stocks.Include(stock => stock.History).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Stock> GetByCodeOrByNameAsync(string input)
        {
            return await _context.Stocks.Include(s => s.History).FirstOrDefaultAsync(e => e.Code == input || e.Name == input).ConfigureAwait(false);
        }

        public async Task<Stock> GetById(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
        }

        public async Task<Stock> Remove(Stock entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<Stock> Update(Stock entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
