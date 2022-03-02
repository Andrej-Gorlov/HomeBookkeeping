using HomeBookkeepingWebApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionDTO>> Get();
        Task<TransactionDTO> Add(TransactionDTO entity);
        Task<bool> Delete(int id);
        Task<bool> Delete(DateTime data);
    }
}
