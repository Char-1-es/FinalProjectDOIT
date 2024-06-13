using FinalProjectDOIT.Entities;

namespace FinalProjectDOIT.Repos.Interfaces
{
    public interface IRepository<T, TId> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(TId id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TId id);

        Task<Entities.Topic> GetOneWithCommentsAsync(int id);
    }
}