using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IUserService : IBaseService<UserDTO>
    {
        Task<IBaseResponse<UserDTO>> Service_GetByFullName(string fullName);
    }
}
