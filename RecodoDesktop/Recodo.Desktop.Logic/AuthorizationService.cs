﻿using Microsoft.AspNetCore.WebUtilities;
using Recodo.Desktop.Models.Auth;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class AuthorizationService
    {
        readonly private DefaultBrowser _browser;
        readonly private string _redirect_url = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";

        readonly Dictionary<string, string> authRequestParams = new();
        public string Endpoint { get; set; }

        public AuthorizationService()
        {
            authRequestParams.Add("redirect_url", _redirect_url);

            _browser = new DefaultBrowser(GetAuthRequestUrl(), _redirect_url);          
        }

        public async Task<Token> Authorize()
        {
            string result = await _browser.InvokeAsync();
            return ParseRawAuthResult(result);
        }

        public string GetAuthRequestUrl()
        {
            return QueryHelpers.AddQueryString(Endpoint, authRequestParams);
        }

        private static Token ParseRawAuthResult(string result)
        {            
            var queryParams = QueryHelpers.ParseNullableQuery(result);
            return new Token(queryParams["access_token"], string.Empty );               
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
