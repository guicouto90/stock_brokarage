using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByNumberAsync(int number);

        Task<Account> GetByNumberWithTransactionHistoryAsync(int number);
        Task<Account> GetLastAccount();
        Task<Account> GetByCustomerId(int id);

        Task<Account> GetByCustomerIdWithWalletAsync(int customerId);
    }
}
