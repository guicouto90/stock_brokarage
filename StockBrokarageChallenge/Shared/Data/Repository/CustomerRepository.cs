using Microsoft.EntityFrameworkCore;
using StockBrokarageChallenge.Application.Shared.Data.Context;
using StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces;
using StockBrokarageChallenge.Application.Shared.Models;


namespace StockBrokarageChallenge.Application.Shared.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> Create(Customer entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<ICollection<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            return await _context.Customers
                .Include(e => e.Account)
                .FirstOrDefaultAsync(c => c.Cpf == cpf).ConfigureAwait(false);
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers
                .Include(e => e.Account)
                .FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);
        }

        public async Task<Customer> Remove(Customer entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<Customer> Update(Customer entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }
    }
}
