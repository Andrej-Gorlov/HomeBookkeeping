using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Entity;

namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionDTO>> GetAsync();
        Task<TransactionDTO> GetByIdAsync(int id);
        Task<TransactionDTO> AddAsync(TransactionDTO entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(DateTime data);
    }
}
