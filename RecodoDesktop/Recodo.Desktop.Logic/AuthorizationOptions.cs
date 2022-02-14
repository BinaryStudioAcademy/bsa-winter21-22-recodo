using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;

namespace Recodo.Desktop.Logic
{
    public class AuthorizationOptions
    {
        const uint bufferSize = 64;
        const string clientId = "sample-client-id";
        const string codeChallengeMethod = "S256";
        const string authorizationEndpoint = "https://oauth.mocklab.io/oauth/authorize";
        readonly string tokenEndpoint = "https://oauth.mocklab.io/oauth/token";
        readonly string userInfoEndpoint = "https://oauth.mocklab.io/userinfo";

        readonly Dictionary<string, string> authRequestParams = new();
        readonly Dictionary<string, string> tokenRequestParams = new();
        public string RedirectUrl { get; set; }
        public string TokenEndpoint => tokenEndpoint;

        public string UserInfoEndpoint => userInfoEndpoint;

        public AuthorizationOptions()
        {
            var state = Base64UrlString.RandomBase64UrlString(bufferSize);
            var codeVerifier = Base64UrlString.RandomBase64UrlString(bufferSize);
            var codeChallenge = Base64UrlString.Base64UrlEncodeNoPadding(Base64UrlString.Sha256(codeVerifier));

            authRequestParams.Add("response_type", "code");
            authRequestParams.Add("client_id", clientId);
            authRequestParams.Add("code_challenge", codeChallenge);
            authRequestParams.Add("code_challenge_method", codeChallengeMethod);
            authRequestParams.Add("state", state);

            tokenRequestParams.Add("grant_type", "code");
            tokenRequestParams.Add("client_id", clientId);
            tokenRequestParams.Add("code_verifier", codeVerifier);
            tokenRequestParams.Add("redirect_url", RedirectUrl);
        }

        public string GetAuthRequestUrl()       
        {
            authRequestParams.Add("redirect_uri", RedirectUrl);
            return QueryHelpers.AddQueryString(authorizationEndpoint, authRequestParams);
        }

        public Dictionary<string, string> GetTokenRequestData(string code)
        {
            tokenRequestParams.Add("code", code);
            return tokenRequestParams;
        }
    }
}
