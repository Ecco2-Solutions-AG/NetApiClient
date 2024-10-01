using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Ecco2.Cloud.PublicApi.Client.V3.Entities;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
internal abstract class ApiClientBase
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
    public async Task AuthenticateAsync(CancellationToken cancellationToken = default)
    {
        var token = await new AuthenticationClient(ClientConfiguration).AuthenticateAsync(cancellationToken);
        HttpClient.DefaultRequestHeaders.Add("Authorization", $"{token.TokenType} {token.Token}");
    }

    /// <summary>
    /// Gets the API client's configuration
    /// </summary>
    protected ApiClientConfiguration ClientConfiguration { get; }
}