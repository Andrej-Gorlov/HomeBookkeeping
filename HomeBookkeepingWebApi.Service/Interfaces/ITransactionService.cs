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
        Task<IBaseResponse<IEnumerable<TransactionDTO>>> Service_Get();
        Task<IBaseResponse<TransactionDTO>> Service_Add(TransactionDTO entity);
        Task<IBaseResponse<bool>> Service_Delete(int id);
        Task<IBaseResponse<bool>> Service_Delete(DateTime data); 
    }
}
