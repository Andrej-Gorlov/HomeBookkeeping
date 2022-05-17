
namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
