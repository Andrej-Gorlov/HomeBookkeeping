using HomeBookkeepingWebApi.Domain.Paging;
using HomeBookkeepingWebApi.Domain.Response;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IBaseService<T>
    {
        Task<IBaseResponse<PagedList<T>>> GetServiceAsync(PagingQueryParameters paging);
        Task<IBaseResponse<T>> GetByIdServiceAsync(int id);
        Task<IBaseResponse<T>> CreateServiceAsync(T entity);
        Task<IBaseResponse<T>> UpdateServiceAsync(T entity);
        Task<IBaseResponse<bool>> DeleteServiceAsync(int id);
    }
}
