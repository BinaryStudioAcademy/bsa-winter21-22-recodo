using Newtonsoft.Json;
using Recodo.Desktop.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class AuthorizationService
    {
        readonly private DefaultBrowser _browser;
        readonly private AuthorizationOptions _options;
        private AuthorizeResult _authResult;
        public AuthorizationService(AuthorizationOptions options)
        {
            _options = options;
            _options.RedirectUrl = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
            _browser = new DefaultBrowser(_options);          
        }

        public async Task<AuthorizeResult> Authorize()
        {
            string result = await _browser.InvokeAsync();
            _authResult = ParseRawAuthResult(result);
            return _authResult;
        }

        public async Task<string> GetToken()
        {
            var data = new StringContent(_options.GetTokenRequestData(_authResult.Code), Encoding.UTF8, "application/x-www-form-urlencoded");
            
            using var client = new HttpClient();
            var response = await client.PostAsync(_options.TokenEndpoint, data);

            string result = response.Content.ReadAsStringAsync().Result;
            Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            return tokenEndpointDecoded["access_token"];
        }

        public async Task<string> GetUserInfo(string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            var content = await client.GetStringAsync(_options.UserInfoEndpoint);
            return content;
        }

        private static AuthorizeResult ParseRawAuthResult(string result)
        {            
            // parse query string
            Dictionary<string, string> keyValuePairs =
                    result.Split('&').ToDictionary(c => c.Split('=')[0].TrimStart('?'),
                                                    c => Uri.UnescapeDataString(c.Split('=')[1]));
            if (keyValuePairs.ContainsKey("code") && keyValuePairs.ContainsKey("state"))
            {
                return new AuthorizeResult { Code = keyValuePairs["code"], State = keyValuePairs["state"] };               
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
