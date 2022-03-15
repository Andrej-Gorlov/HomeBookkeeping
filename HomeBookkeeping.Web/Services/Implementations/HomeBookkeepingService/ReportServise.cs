using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;

namespace HomeBookkeeping.Web.Services.Implementations.HomeBookkeepingService
{
    public class ReportServise: BaseService, IReportServise
    {
        private readonly IHttpClientFactory _clientFactory;
        public ReportServise(IHttpClientFactory clientFactory) : base(clientFactory) => _clientFactory = clientFactory;



        public async Task<T> ExpensCategoryFullYaerReportAsync<T>(string category)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/"+ category
            });
        }

        public async Task<T> ExpensCategoryYaerReportAsync<T>(string category, int year)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/" + category+"/"+ year
            });
        }

        public async Task<T> ExpensFullReportAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report"
            });
        }

        public async Task<T> ExpensNameCategoryYearMonthReportAsync<T>(string category, string fullName, int year, string month)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ExpensNameCategoryYearMonthReport/" + category + "/" + fullName+"/"+ year+"/"+ month
            });
        }

        public async Task<T> ExpensNameCategoryYearReportAsync<T>(string category, string fullName, int year)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ExpensNameCategoryYearReport/" + category + "/" + fullName + "/" + year 
            });
        }

        public async Task<T> ExpensNameFullYearReportAsync<T>(string fullName)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/" + fullName
            });
        }


        public async Task<T> ExpensNameYearMonthReportAsync<T>(string fullName, int year, string month)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ExpensNameYearMonthReport/" + fullName + "/" +year+"/"+month
            });
        }

        public async Task<T> ExpensNameYearReportAsync<T>(string fullName, int year)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ExpensNameYearReport/" + fullName + "/" + year
            });
        }

        public async Task<T> GetAllCategory<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/listCategory"
            });
        }

        public async Task<T> GetAllFullNameUser<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/listFullNameUser"
            });
        }
    }
}
