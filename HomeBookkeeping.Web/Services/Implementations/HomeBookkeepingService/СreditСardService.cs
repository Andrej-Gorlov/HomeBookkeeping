using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;

namespace HomeBookkeeping.Web.Services.Implementations.HomeBookkeepingService
{
    public class СreditСardService : BaseService, IСreditСardService
    {
        private readonly IHttpClientFactory _clientFactory;
        public СreditСardService(IHttpClientFactory clientFactory):base(clientFactory)=> _clientFactory = clientFactory;



        public async Task<T> CreateСreditСardAsync<T>(СreditСardDTOBase creditСardDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.POST,
                Data = creditСardDTO,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcard",
            });
        }

        public async Task<T> DeleteСreditСardAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.DELETE,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcard/" + id
            });
        }

        public async Task<T> EnrollmentСreditСardAsync<T>(string nameCard, string number, decimal sum)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.POST,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcard/"+ nameCard+"/"+ number +"/"+sum
            });
        }

        public async Task<T> GetByIdСreditСardAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcard/" + id
            });
        }

        public async Task<T> GetСreditСardsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcards"
            });
        }

        public async Task<T> GetСreditСardsAsync<T>(string fullName)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcards/listСreditСardsUser/" + fullName
            });
        }

        public async Task<T> UpdateСreditСardAsync<T>(СreditСardDTOBase creditСardDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.PUT,
                Data = creditСardDTO,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/creditcard"
            });
        }
    }
}
