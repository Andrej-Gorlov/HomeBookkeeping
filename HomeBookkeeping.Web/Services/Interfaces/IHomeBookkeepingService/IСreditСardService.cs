using HomeBookkeeping.Web.Models.HomeBookkeeping;
using HomeBookkeeping.Web.Models.Paging;

namespace HomeBookkeeping.Web.Services.Interfaces.IHomeBookkeepingService
{
    public interface IСreditСardService : IBaseService
    {
        Task<T> GetСreditСardsAsync<T>(PagingParameters parameters);
        Task<T> GetСreditСardsAsync<T>(string fullName);
        Task<T> GetByIdСreditСardAsync<T>(int id);
        Task<T> CreateСreditСardAsync<T>(СreditСardDTOBase creditСardDTO);
        Task<T> UpdateСreditСardAsync<T>(СreditСardDTOBase creditСardDTO);
        Task<T> DeleteСreditСardAsync<T>(int id);
        Task<T> EnrollmentСreditСardAsync<T>(string nameCard, string number, decimal sum);
    }
}
