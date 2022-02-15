using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Recodo.Desktop.Logic
{
    public class AuthorizationOptions
    {
        const string clientId = "sample-client-id";
        const string authorizationEndpoint = "http://127.0.0.1:4000/register";

        readonly Dictionary<string, string> authRequestParams = new();
        public string RedirectUrl { get;}

        public AuthorizationOptions()
        {
            RedirectUrl = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
            authRequestParams.Add("response_type", "token");
            authRequestParams.Add("client_id", clientId);
            authRequestParams.Add("redirect_url", RedirectUrl);
        }

        public string GetAuthRequestUrl()       
        {        
            return QueryHelpers.AddQueryString(authorizationEndpoint, authRequestParams);
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
