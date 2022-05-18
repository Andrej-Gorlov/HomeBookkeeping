using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using Microsoft.AspNetCore.Http;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface ITransactionService
    {
        Task<IBaseResponse<IEnumerable<TransactionDTO>>> GetServiceAsync();
        Task<IBaseResponse<TransactionDTO>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<TransactionDTO>> AddServiceAsync(TransactionDTO entity);
        Task<IBaseResponse<bool>> DeleteServiceAsync(int id);
        Task<IBaseResponse<bool>> DeleteServiceAsync(DateTime data);
        Task<IBaseResponse<List<TransactionDTO>>> AddFileExcelServiceAsync(IFormFile fileExcel, string userFullName, string numberCardUser);
    }
}
