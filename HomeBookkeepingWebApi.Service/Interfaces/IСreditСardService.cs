using HomeBookkeepingWebApi.Domain.DTO;
using HomeBookkeepingWebApi.Domain.Response;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IСreditСardService : IBaseService<СreditСardDTO>
    {
        Task<IBaseResponse<СreditСardDTO>> EnrollmentServiceAsync(string nameBank, string number, decimal sum);
        Task<IBaseResponse<IEnumerable<СreditСardDTO>>> GetServiceAsync(string fullName);
    }
}
