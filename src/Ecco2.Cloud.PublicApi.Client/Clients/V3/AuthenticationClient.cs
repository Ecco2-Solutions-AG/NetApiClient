using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

internal class AuthenticationClient
{
    public async Task<JwtToken> AuthenticateAsync(Ecco2ClientConfiguration clientConfiguration, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri($"{clientConfiguration.BaseAddress.TrimEnd('/')}/authentication/");

        var response = await client.PostAsJsonAsync("token", new
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