using HomeBookkeepingWebApi.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface IСreditСardRepository : IBaseRepository<СreditСardDTO>
    {
        Task<СreditСardDTO> Enrollment(string nameBank, string number, decimal sum);
        Task<IEnumerable<СreditСardDTO>> Get(string fullName);
    }
}
