using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class AuthRequestOptions
    {        
        const string clientId = "sample-client-id";
        const string codeChallengeMethod = "S256";
        const string authorizationEndpoint = "https://oauth.mocklab.io/oauth/authorize";
        const string tokenEndpoint = "https://oauth.mocklab.io/oauth/token";
        const string userInfoEndpoint = "https://oauth.mocklab.io/userinfo";

        string state;
        string codeVerifier;
        string codeChallenge;

        public string RedirectUrl { get; set; }
        public AuthRequestOptions()
        {
            
        }

        public string GetAuthRequestUrl()
        {
            return $"{authorizationEndpoint}?response_type=code&client_id={clientId}&" +
                   $"redirect_uri={Uri.EscapeDataString(RedirectUrl)}&code_challenge={codeChallenge}&" +
                                          $"code_challenge_method={codeChallengeMethod}&state={state}";
        }
    }
}
