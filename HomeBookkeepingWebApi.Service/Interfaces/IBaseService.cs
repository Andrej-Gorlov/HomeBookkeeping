using HomeBookkeepingWebApi.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBookkeepingWebApi.Service.Interfaces
{
    public interface IBaseService<T>
    {
        Task<IBaseResponse<IEnumerable<T>>> ServiceGet();
        Task<IBaseResponse<T>> ServiceGetById(int id);
        Task<IBaseResponse<T>> ServiceCreate(T entity);
        Task<IBaseResponse<T>> ServiceUpdate(T entity);
        Task<IBaseResponse<bool>> ServiceDelete(int id);
    }
}
