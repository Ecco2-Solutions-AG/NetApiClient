using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
internal abstract class ApiClientBase: IApiClient
{
    /// <summary>
    /// The HttpClient that is going to be used
    /// </summary>
    protected HttpClient HttpClient { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DataBrokerClient "/> class.
    /// </summary>
    protected ApiClientBase(ApiClientConfiguration clientConfiguration)
    {
        ClientConfiguration = clientConfiguration;

        var handler = new HttpClientHandler();
        HttpClient = new HttpClient (clientConfiguration.ConfigureMessageHandler?.Invoke(handler) ?? handler);
    }

    /// <summary>
    /// Authenticates the client.
    /// </summary>
    public async Task<JwtToken> AuthenticateAsync(CancellationToken cancellationToken = default)
    {
        var token = await new AuthenticationClient(ClientConfiguration).AuthenticateAsync(cancellationToken);
        HttpClient.DefaultRequestHeaders.Add("Authorization", $"{token.TokenType} {token.Token}");

        return token;
    }

    /// <summary>
    /// Gets the API client's configuration
    /// </summary>
    public ApiClientConfiguration ClientConfiguration { get; }
}