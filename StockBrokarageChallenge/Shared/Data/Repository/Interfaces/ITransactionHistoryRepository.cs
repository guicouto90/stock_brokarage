using StockBrokarageChallenge.Application.Shared.Models;

namespace StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces
{
    public interface ITransactionHistoryRepository : IRepository<TransactionHistory>
    {
        Task<ICollection<TransactionHistory>> ListAllByAccountId(int accountId);
    }
}
