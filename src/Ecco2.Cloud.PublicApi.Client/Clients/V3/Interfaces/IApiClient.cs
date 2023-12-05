using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access data endpoints of the Ecco2 public API
/// </summary>
public interface IApiClient
{
    /// <summary>
    /// Authenticates the client.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The JWT token.</returns>
    Task<JwtToken> AuthenticateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the API client's configuration
    /// </summary>
    public ApiClientConfiguration ClientConfiguration { get; }
}