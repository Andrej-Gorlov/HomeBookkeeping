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
        Task<IBaseResponse<IEnumerable<T>>> Service_Get();
        Task<IBaseResponse<T>> Service_GetById(int id);
        Task<IBaseResponse<T>> Service_Create(T entity);
        Task<IBaseResponse<T>> Service_Update(T entity);
        Task<IBaseResponse<bool>> Service_Delete(int id);
    }
}
