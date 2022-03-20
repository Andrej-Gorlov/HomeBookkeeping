using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService;

namespace HomeBookkeeping.Web.Services.Implementations.HomeBookkeepingService
{
    public class UserService: BaseService, IUserService
    {
        private readonly IHttpClientFactory _clientFactory;
        public UserService(IHttpClientFactory clientFactory) : base(clientFactory) => _clientFactory = clientFactory;



        public async Task<T> CreateUserAsync<T>(UserDTOBase userDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.POST,
                Data = userDTO,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/user",
            });
        }

        public async Task<T> DeleteUserAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.DELETE,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/user/" + id
            });
        }

        public async Task<T> GetByIdUserAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/user/" + id
            });
        }

        public async Task<T> GetFullNameUserAsync<T>(string fullName)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/user/" + fullName
            });
        }

        public async Task<T> GetUsersAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.GET,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/users"
            });
        }

        public async Task<T> UpdateUserAsync<T>(UserDTOBase userDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                Api_Type = StaticDitels.ApiType.PUT,
                Data = userDTO,
                Url = StaticDitels.HomeBookkeepingApiBase + "/api/user"
            });
        }
    }
}
