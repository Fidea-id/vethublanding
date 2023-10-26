using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using VethubLanding.Interfaces;
using VethubLanding.Models;

namespace VethubLanding.Services
{
    public class RestAPIService : IRestAPIService
    {
        private IUriService _uriService;
        private readonly HttpClient _httpClient;

        public RestAPIService(IUriService uriService, HttpClient httpClient)
        {
            _uriService = uriService;
            _httpClient = httpClient;
        }
        public async Task<T> GetResponse<T>(string url, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri();
            HttpRequestMessage request = new(HttpMethod.Get, getUrl + url);
            if (auth != null)
            {
                request.Headers.Add("Authorization", $"Bearer {auth}");
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var test = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception($"{errors.Errors[0].Message}", errors.Errors[0].Detail);
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }
        

        public async Task<T> PostResponse<T>(string url, string obj, string? auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri();

            HttpRequestMessage request = new(HttpMethod.Post, getUrl + url);
            request.Content = new StringContent(obj, Encoding.UTF8, "application/json");
            if (auth != null)
            {
                request.Headers.Add("Authorization", $"Bearer {auth}");
            }

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            var test = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception($"{errors.Errors[0].Message}", errors.Errors[0].Detail);
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        public async Task<string> PostResponseFile(string url, IFormFile file, string? auth = null)
        {
            Uri getUrl = _uriService.GetAPIUri();

            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = $"\"{file.FileName}\""
            };

            MultipartFormDataContent formData = new();
            formData.Add(fileContent, "file", file.FileName);
            HttpRequestMessage request = new(HttpMethod.Post, getUrl + url)
            {
                Content = formData
            };

            if (auth != null)
            {
                request.Headers.Add("Authorization", $"Bearer {auth}");
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception($"{errors.Errors[0].Message}", errors.Errors[0].Detail);
            }

            var readResponse = await response.Content.ReadAsStringAsync();
            return readResponse;
        }

        public async Task<T> PutResponse<T>(string url, int id, string obj, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri();
            var UrlId = id == 0 ? string.Empty : $"/{id}";

            HttpRequestMessage request = new(HttpMethod.Put, $"{getUrl + url + UrlId}");
            request.Content = new StringContent(obj, Encoding.UTF8, "application/json");
            if (auth != null)
            {
                request.Headers.Add("Authorization", $"Bearer {auth}");
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception($"{errors.Errors[0].Message}", errors.Errors[0].Detail);
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }

        public async Task<T> DeleteResponse<T>(string url, int id, string auth = null) where T : class
        {
            Uri getUrl = _uriService.GetAPIUri();
            HttpRequestMessage request = new(HttpMethod.Delete, $"{getUrl + url}/{id}");
            if (auth != null)
            {
                request.Headers.Add("Authorization", $"Bearer {auth}");
            }
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            var test = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var errors = JsonConvert.DeserializeObject<ErrorResponseModel>(await response.Content.ReadAsStringAsync());
                throw new Exception($"{errors.Errors[0].Message}", errors.Errors[0].Detail);
            }
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? default;
        }
    }
}