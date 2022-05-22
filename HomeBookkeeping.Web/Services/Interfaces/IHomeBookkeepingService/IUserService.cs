using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;

namespace HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService
{
    public interface IUserService : IBaseService
    {
        Task<T> GetUsersAsync<T>(PagingParameters parameters);
        Task<T> GetByIdUserAsync<T>(int id);
        Task<T> CreateUserAsync<T>(UserDTOBase userDTO);
        Task<T> UpdateUserAsync<T>(UserDTOBase userDTO);
        Task<T> DeleteUserAsync<T>(int id);
        Task<T> GetFullNameUserAsync<T>(string fullName);
    }
}
