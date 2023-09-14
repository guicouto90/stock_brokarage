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
            await _context.SaveChangesAsync();
            var newAccount = await _context.Accounts.Where(e => e.AccountNumber == entity.AccountNumber).FirstOrDefaultAsync();
            return newAccount;
        }

        public async Task<ICollection<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByCustomerId(int id)
        {
            return await _context.Accounts
                .Where(a => a.CustomerId == id)
                .Include(a => a.TransactionHistories)
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetByCustomerIdWithWalletAsync(int customerId)
        {
            return await _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .Include(a => a.Wallet)
                .Include(a => a.Wallet.StocksWallet)
                    .ThenInclude(sw => sw.Stock)
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetById(int id)
        {
            return await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Account> GetByNumberAsync(int number)
        {
            return await _context.Accounts.Where(a => a.AccountNumber == number).FirstOrDefaultAsync();
        }

        public async Task<Account> GetByNumberWithTransactionHistoryAsync(int number)
        {
            return await _context.Accounts
                .Where(a => a.AccountNumber == number)
                .Include(a => a.TransactionHistories)
                .FirstOrDefaultAsync();
        }

        public async Task<Account> GetLastAccount()
        {
            return await _context.Accounts.OrderBy(a => a.AccountNumber).LastOrDefaultAsync();
        }

        public async Task<Account> Remove(Account entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Account> Update(Account entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            var account = await _context.Accounts.Where(e => e.AccountNumber == entity.AccountNumber).FirstOrDefaultAsync();
            return account;
        }
    }
}
