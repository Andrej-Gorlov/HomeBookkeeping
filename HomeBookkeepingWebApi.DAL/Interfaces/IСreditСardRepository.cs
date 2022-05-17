using HomeBookkeepingWebApi.Domain.DTO;

namespace HomeBookkeepingWebApi.DAL.Interfaces
{
    public interface IСreditСardRepository : IBaseRepository<СreditСardDTO>
    {
        Task<СreditСardDTO> EnrollmentAsync(string nameBank, string number, decimal sum);
        Task<IEnumerable<СreditСardDTO>> GetAsync(string fullName);
    }
}
