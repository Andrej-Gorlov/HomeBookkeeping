using HomeBookkeeping.Web.Models;

namespace HomeBookkeeping.Web.Services.Interfaces
{
    public interface IBaseService : IDisposable
    {
        ResponseBase responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);// отправка request
    }
}
