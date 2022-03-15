using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IСreditСardService : IBaseService<СreditСardDTO>
    {
        Task<IBaseResponse<СreditСardDTO>> Service_Enrollment(string nameBank, string number, decimal sum);
        Task<IBaseResponse<IEnumerable<СreditСardDTO>>> Service_Get(string fullName);
    }
}
