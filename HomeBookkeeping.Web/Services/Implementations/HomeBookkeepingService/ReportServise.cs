using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.Paging;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;

namespace HomeBookkeeping.Web.Services.Implementations.HomeBookkeepingService
{
    public class ReportServise: BaseService, IReportServise
    {
        private readonly IHttpClientFactory _clientFactory;
        public ReportServise(IHttpClientFactory clientFactory) : base(clientFactory) => _clientFactory = clientFactory;



        public async Task<T> ReportByCategoryAllYearsAsync<T>(PagingParameters parameters ,string category)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportByCategoryAllYears/" + category + "?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
            });
        }

        public async Task<T> ReportByCategoryYaerAsync<T>(PagingParameters parameters, string category, int year)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportByCategoryYaer/" + category+"/"+ year + "?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
            });
        }

        public async Task<T> FullReportAsync<T>(PagingParameters parameters)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/full?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
            });
        }

        public async Task<T> ReportByCategoryNameUserYearMonthAsync<T>(string category, string fullName, int year, string month)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportByCategoryNameUserYearMonth/" + category + "/" + fullName+"/"+ year+"/"+ month
            });
        }

        public async Task<T> ReportByCategoryNameUserYearAsync<T>(PagingParameters parameters, string category, string fullName, int year)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportByCategoryNameUserYear/" + category + "/" + fullName + "/" + year + "?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
            });
        }

        public async Task<T> ReportAllYearsNameUserAsync<T>(PagingParameters parameters, string fullName)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportAllYearsNameUser/" + fullName + "?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
            });
        }

        public async Task<T> ReportByNameUserYearMonthAsync<T>(string fullName, int year, string month)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportByNameUserYearMonth/" + fullName + "/" +year+"/"+month
            });
        }

        public async Task<T> ReportByNameUserAsync<T>(PagingParameters parameters, string fullName, int year)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/report/ReportByNameUserYear/" + fullName + "/" + year + "?pagenumber=" + parameters.PageNumber + "&pagesize=" + parameters.PageSize
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
