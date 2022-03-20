using static HomeBookkeeping.Web.StaticDitels;

namespace HomeBookkeeping.Web.Models
{
    public class ApiRequest
    {
        public ApiType Api_Type { get; set; } = ApiType.GET;
        public string? Url { get; set; } // url adres по которму отпровляем request api
        public object? Data { get; set; } // передача data в теле messenger
        public string? AccessToken { get; set; }//для идентификации
    }
}
