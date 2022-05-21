using static HomeBookkeeping.Web.StaticDitels;

namespace HomeBookkeeping.Web.Models
{
    public class ApiRequest
    {
        public ApiType Api_Type { get; set; } = ApiType.GET;
        public string? Url { get; set; } 
        public object? Data { get; set; }
        public string? AccessToken { get; set; }
        public IFormFile? File { get; set; }
    }
}
