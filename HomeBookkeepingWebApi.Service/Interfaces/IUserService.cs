using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IUserService : IBaseService<UserDTO>
    {
        Task<IBaseResponse<UserDTO>> GetByFullNameServiceAsync(string fullName);
    }
}
