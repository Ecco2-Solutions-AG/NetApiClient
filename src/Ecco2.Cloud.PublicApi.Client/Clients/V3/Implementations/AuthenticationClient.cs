using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

internal class AuthenticationClient: ApiClientBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataBrokerClient "/> class.
    /// </summary>
    public AuthenticationClient(ApiClientConfiguration clientConfiguration) : base(clientConfiguration) { }


    public async Task<JwtToken> AuthenticateAsync(ApiClientConfiguration clientConfiguration, CancellationToken cancellationToken)
    {
        HttpClient.BaseAddress = new Uri($"{clientConfiguration.AuthBaseAddress.TrimEnd('/')}/auth/");

        var response = await HttpClient.PostAsJsonAsync("token", new
        {
            clientId = clientConfiguration.ClientId,
            clientSecret = clientConfiguration.ClientSecret
        }, cancellationToken);
        response.EnsureSuccessStatusCode();
            
        var jwtToken = await response.Content.ReadFromJsonAsync<JwtToken>(cancellationToken);
        if (jwtToken?.Token is null) { throw new UnauthorizedAccessException("AccessToken is null"); }
        return jwtToken;
    }
}