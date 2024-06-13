using System.Security.Cryptography;

namespace FinalProjectDOIT.Repos.Interfaces
{
    public interface IUserInterface<T, TId>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetOneAsync(TId id);
        Task LockoutUser(TId id);
    }
}