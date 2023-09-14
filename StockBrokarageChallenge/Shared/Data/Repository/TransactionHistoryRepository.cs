using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TransactionHistory> Create(TransactionHistory entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TransactionHistory>> GetAll()
        {
            return await _context.TransactionsHistory.ToListAsync();
        }

        public async Task<TransactionHistory> GetById(int id)
        {
            return await _context.TransactionsHistory.Where(th => th.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<TransactionHistory>> ListAllByAccountId(int accountId)
        {
            return await _context.TransactionsHistory.Where(th => th.AccountId == accountId).ToListAsync();
        }

        public async Task<TransactionHistory> Remove(TransactionHistory entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TransactionHistory> Update(TransactionHistory entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
