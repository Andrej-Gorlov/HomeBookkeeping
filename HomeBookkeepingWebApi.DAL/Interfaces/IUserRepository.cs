using HomeBookkeepingWebApi.Domain.DTO;

namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserDTO>
    {
        Task<UserDTO> GetByFullNameAsync(string fullName);
    }
}
