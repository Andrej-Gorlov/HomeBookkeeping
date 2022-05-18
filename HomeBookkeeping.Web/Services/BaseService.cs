using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace HomeBookkeeping.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseBase responseModel {get;set;}
        public IHttpClientFactory? httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseBase();
            this.httpClient = httpClient;
        }


        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("HomeBookkeepingAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    // заполнение всех данных запроса(response)
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? apiResponse = null;//Ответ-Response
                switch (apiRequest.Api_Type)
                {
                    case StaticDitels.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDitels.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case StaticDitels.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                //create response к api
                apiResponse = await client.SendAsync(message);
                var apiContet = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContet);
                return apiResponseDto;
            }
            catch (Exception ex)
            {
                var dto = new ResponseBase
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    //IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponse = JsonConvert.DeserializeObject<T>(res);
                return apiResponse;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
