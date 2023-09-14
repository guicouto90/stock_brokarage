namespace StockBrokarageChallenge.Application.Shared.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T entity);

        Task<ICollection<T>> GetAll(); 
        Task<T> GetById(int id);
        Task<T> Update(T entity);
        Task<T> Remove(T entity);
    }
}
