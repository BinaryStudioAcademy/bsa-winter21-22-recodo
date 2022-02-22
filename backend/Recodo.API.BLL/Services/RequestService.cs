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
        private static HttpClient Client { get; set; } = new HttpClient();

        private static string BaseUrl = new string(@"https://localhost:44316/api/");

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

        public async Task SendGetRequest(int id, string token)
        {
            Client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, BaseUrl +
                $"File?id={id}"));
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Invalid user");
            }
        }

        public async Task SendFinishRequest(int videoId)
        {
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Put, BaseUrl +
                $"File?id={videoId}"));
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Error");
            }
        }


    }
}
