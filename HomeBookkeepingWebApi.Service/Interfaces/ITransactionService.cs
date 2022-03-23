using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<IBaseResponse<IEnumerable<TransactionDTO>>> ServiceGet();
        Task<IBaseResponse<TransactionDTO>> ServiceGetById(int id);
        Task<IBaseResponse<TransactionDTO>> ServiceAdd(TransactionDTO entity);
        Task<IBaseResponse<bool>> ServiceDelete(int id);
        Task<IBaseResponse<bool>> ServiceDelete(DateTime data); 
    }
}
