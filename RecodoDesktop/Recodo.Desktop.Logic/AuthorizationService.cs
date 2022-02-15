using Microsoft.AspNetCore.WebUtilities;
using Recodo.Desktop.Models.Auth;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class AuthorizationService
    {
        readonly private DefaultBrowser _browser;
        readonly private AuthorizationOptions _options;
        public AuthorizationService(AuthorizationOptions options)
        {
            _options = options;
            _browser = new DefaultBrowser(_options);          
        }

        public async Task<Token> Authorize()
        {
            string result = await _browser.InvokeAsync();
            return ParseRawAuthResult(result);
        }

        private static Token ParseRawAuthResult(string result)
        {            
            var queryParams = QueryHelpers.ParseNullableQuery(result);
            return new Token(queryParams["access_token"], string.Empty );               
        }
    }
}
