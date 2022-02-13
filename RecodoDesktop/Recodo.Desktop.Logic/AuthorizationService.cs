using Recodo.Desktop.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class AuthorizationService
    {
        private DefaultBrowser _browser;
        private AuthorizationOptions _options;
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
            _authResult = ParseRawResult(result);
            return _authResult;
        }

        private static AuthorizeResult ParseRawResult(string result)
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
