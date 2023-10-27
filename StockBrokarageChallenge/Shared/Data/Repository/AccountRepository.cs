using AutoMapper.Execution;
using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> Create(Account entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<ICollection<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByCustomerId(int id)
        {
            return await _context.Accounts
                .Include(a => a.TransactionHistories)
                .FirstOrDefaultAsync(a => a.CustomerId == id)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetByCustomerIdWithWalletAsync(int customerId)
        {
            return await _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .Include(a => a.Wallet)
                .Include(a => a.Wallet.StocksWallet)
                    .ThenInclude(sw => sw.Stock)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<Account> GetById(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
        }

        public async Task<Account> GetByNumberAsync(int number)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == number).ConfigureAwait(false);
        }

        public async Task<Account> GetByNumberWithTransactionHistoryAsync(int number)
        {
            return await _context.Accounts
                .Include(a => a.TransactionHistories)
                .FirstOrDefaultAsync(a => a.AccountNumber == number)
                .ConfigureAwait(false);
        }

        public async Task<Account> GetLastAccount()
        {
            return await _context.Accounts.OrderBy(a => a.AccountNumber).LastOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<Account> Remove(Account entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<Account> Update(Account entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
