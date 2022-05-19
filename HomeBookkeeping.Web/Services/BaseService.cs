using HomeBookkeeping.Web.Models;
using HomeBookkeeping.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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
                if (apiRequest.File!=null && apiRequest.File.Length>0)
                {
                    var result = await SendFile(apiRequest);
                    var contet = await result.Content.ReadAsStringAsync();
                    var responseDto = JsonConvert.DeserializeObject<T>(contet);
                    return responseDto;
                }
                var client = httpClient.CreateClient("HomeBookkeepingAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? apiResponse = null;
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
        private async Task<HttpResponseMessage> SendFile(ApiRequest apiRequest)
        {
            var client = httpClient.CreateClient();
            using (var memoryStream = new MemoryStream())
            {
                //Get the file steam from the multiform data uploaded from the browser
                await apiRequest.File.CopyToAsync(memoryStream);

                //Build an multipart/form-data request to upload the file to Web API
                using var form = new MultipartFormDataContent();
                using var fileContent = new ByteArrayContent(memoryStream.ToArray());
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                form.Add(fileContent, "file", apiRequest.File.FileName);

                return await client.PostAsync(apiRequest.Url, form);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
