using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;

namespace HomeBookkeeping.Web.Services.Implementations.HomeBookkeepingService
{
    public class TransactionService: BaseService, ITransactionService
    {
        private readonly IHttpClientFactory _clientFactory;
        public TransactionService(IHttpClientFactory clientFactory) : base(clientFactory) => _clientFactory = clientFactory;


        public async Task<T> AddTransactionAsync<T>(TransactionDTOBase transactionDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.POST,
                Data = transactionDTO,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/transaction",
            });
        }
        public async Task<T> AddTransactionFromFileExcelAsync<T>(IFormFile file, string userFullName, string numberCardUser)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.POST,
                File=file,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/transaction/" + userFullName + "/" + numberCardUser 
            });
        }
        public async Task<T> DeleteTransactionAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.DELETE,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/transaction/" + id
            });
        }
        public async Task<T> DeleteTransactionAsync<T>(int year, int month, int day, int hour, int minute, int second)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.DELETE,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/transaction/" + year + "/" + month + "/" + day+"/"+ hour + "/" + minute + "/" + second
            });
        }
        public async Task<T> GetByIdTransactionAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/transaction/" + id
            });
        }
        public async Task<T> GetTransactionsAsync<T>(PagingParameters parameters)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/transactions?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
            });
        }

    }
}
