using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

internal class AuthenticationClient
{
    // the HttpClient that is going to be used
    private readonly HttpClient _httpClient;

    // the API client's configuration
    private readonly ApiClientConfiguration _clientConfiguration;


    /// <summary>
    /// Initializes a new instance of the <see cref="DataBrokerClient "/> class.
    /// </summary>
    public AuthenticationClient(ApiClientConfiguration clientConfiguration)
    {
        _clientConfiguration = clientConfiguration;

        var handler = new HttpClientHandler();
        _httpClient = new HttpClient (clientConfiguration.ConfigureMessageHandler?.Invoke(handler) ?? handler);
    }


    
    /// <summary>
    /// Authenticates the client.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The JWT token to be used for all subsequent calls to the API server.</returns>
    public async Task<JwtToken> AuthenticateAsync(CancellationToken cancellationToken)
    {
        _httpClient.BaseAddress = new Uri($"{_clientConfiguration.BaseAddress.TrimEnd('/')}/auth/");

        var response = await _httpClient.PostAsJsonAsync("token", new
        {
            clientId = _clientConfiguration.ClientId,
            clientSecret = _clientConfiguration.ClientSecret
        }, cancellationToken);
        response.EnsureSuccessStatusCode();
            
        var jwtToken = await response.Content.ReadFromJsonAsync<JwtToken>(cancellationToken);
        if (jwtToken?.Token is null) { throw new UnauthorizedAccessException("AccessToken is null"); }
        return jwtToken;
    }
}