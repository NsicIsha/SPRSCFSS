using System;
using RestSharp.Authenticators;

internal class RestClientOptions : Uri
{
    public RestClientOptions(string uriString) : base(uriString)
    {
    }

    public HttpBasicAuthenticator Authenticator { get; set; }
}