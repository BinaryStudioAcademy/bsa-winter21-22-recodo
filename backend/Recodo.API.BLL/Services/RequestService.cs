using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Recodo.FIle.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.FIle.BLL.Services
{
    public class RequestService : IRequestService
    {
        private HttpClient Client { get; set; } = new HttpClient();
        private readonly IConfiguration _configuration;

        private string BaseUrl;

        public RequestService(IConfiguration configuration)
        {
            _configuration = configuration;
            BaseUrl = _configuration["MainApiUrl"];
        }

        public async Task<string> SendSaveRequest(string token)
        {
            Client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Post, BaseUrl +
                $"File"));
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Error");
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> SendGetRequest(int videoId, string token)
        {
            Client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token.Trim('"','\\'));

            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, BaseUrl +
                $"File?id={videoId}"));
            return response.IsSuccessStatusCode;
        }

        public async Task<string> SendFinishRequest(int videoId)
        {
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Put, BaseUrl +
                $"File?id={videoId}"));
            if (response.IsSuccessStatusCode)
            {
                return videoId.ToString();
            }

            return null;
        }

        public async Task<bool> SendDeleteRequest(int videoId, string token)
        {
            Client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token.Trim('"', '\\'));

            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Delete, BaseUrl +
                $"Videos/{videoId}"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
