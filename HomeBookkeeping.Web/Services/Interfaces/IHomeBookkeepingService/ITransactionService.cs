using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;

namespace HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService
{
    public interface ITransactionService : IBaseService
    {
        Task<T> AddTransactionAsync<T>(TransactionDTOBase transactionDTO);
        Task<T> AddTransactionFromFileExcelAsync<T>(IFormFile fileExcel, string userFullName, string numberCardUser);
        Task<T> DeleteTransactionAsync<T>(int id);
        Task<T> DeleteTransactionAsync<T>(int year, int month, int day, int hour, int minute, int second);
        Task<T> GetTransactionsAsync<T>(PagingParameters parameters);
        Task<T> GetByIdTransactionAsync<T>(int id);
    }
}
