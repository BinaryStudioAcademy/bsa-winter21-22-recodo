﻿using Microsoft.AspNetCore.WebUtilities;
using System;
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

        readonly string state;
        readonly string codeVerifier;
        readonly string codeChallenge;

        public string RedirectUrl { get; set; }
        public string TokenEndpoint => tokenEndpoint;

        public string UserInfoEndpoint => userInfoEndpoint;

        public AuthorizationOptions()
        {
            state = Base64UrlString.RandomBase64UrlString(bufferSize);
            codeVerifier = Base64UrlString.RandomBase64UrlString(bufferSize);
            codeChallenge = Base64UrlString.Base64UrlEncodeNoPadding(Base64UrlString.Sha256(codeVerifier));
        }

        public string GetAuthRequestUrl()
        {
            return $"{authorizationEndpoint}?response_type=code&client_id={clientId}&" +
                   $"redirect_uri={Uri.EscapeDataString(RedirectUrl)}&code_challenge={codeChallenge}&" +
                                          $"code_challenge_method={codeChallengeMethod}&state={state}";
        }

        public string GetTokenRequestData(string code)
        {
            return $"grant_type=code&client_id={clientId}&code={code}&" +
                   $"code_verifier={codeVerifier}&redirect_url={RedirectUrl}";
        }
    }
}
