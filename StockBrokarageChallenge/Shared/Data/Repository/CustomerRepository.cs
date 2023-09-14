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
            await _context.SaveChangesAsync();
            var newCustomer = await _context.Customers.Where(e => e.Cpf == entity.Cpf).FirstOrDefaultAsync();
            return newCustomer;
        }

        public async Task<ICollection<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByCpfAsync(string cpf)
        {
            return await _context.Customers
                .Where(c => c.Cpf == cpf)
                .Include(e => e.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers
                .Where(c => c.Id == id)
                .Include(e => e.Account)
                .FirstOrDefaultAsync();
        }

        public async Task<Customer> Remove(Customer entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity; ;
        }

        public async Task<Customer> Update(Customer entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            var customer = await _context.Customers.Where(e => e.Cpf == entity.Cpf).FirstOrDefaultAsync();
            return customer;
        }
    }
}
