using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
internal class DataBrokerClient: IDataBrokerClient
{
    private readonly Ecco2ClientConfiguration _clientConfiguration;
    private readonly HttpClient _httpClient;

    
    /// <summary>
    /// Initializes a new instance of the <see cref="DataBrokerClient "/> class.
    /// </summary>
    public DataBrokerClient(Ecco2ClientConfiguration clientConfiguration)
    {
        _clientConfiguration = clientConfiguration;

        // the base address must have a trailing '/' in order to be recognized correctly
        _httpClient = new HttpClient { BaseAddress = new Uri($"{clientConfiguration.BaseAddress.TrimEnd('/')}/broker/") };
    }

    /// <summary>
    /// Authenticates the client.
    /// </summary>
    public async Task<JwtToken> AuthenticateAsync(CancellationToken cancellationToken = default)
    {
        var token = await new AuthenticationClient().AuthenticateAsync(_clientConfiguration, cancellationToken);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"{token.TokenType} {token.Token}");

        return token;
    }
    
    /// <summary>
    /// Requests the process point stored at the specified identifier.
    /// </summary>
    public async Task<ProcessPoint> GetAsync(Guid identifier, CancellationToken cancellationToken = default)
    {
        if (identifier == Guid.Empty) { throw new ArgumentException("Identifier must be specified"); }

        var response = await _httpClient.GetAsync($"process-points/{identifier:D}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ProcessPoint>(cancellationToken: cancellationToken, options: new JsonSerializerOptions());        
    }

    /// <summary>
    /// Requests the process points stored at the specified identifiers.
    /// </summary>
    public async Task<ProcessPoint[]> GetAsync(IEnumerable<Guid> identifiers, CancellationToken cancellationToken = default)
    {
        if (identifiers is null) { throw new ArgumentNullException(nameof(identifiers)); }

        var response = await _httpClient.PostAsJsonAsync("process-points/get-range", identifiers.Where(i => i != Guid.Empty), cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ProcessPoint[]>(cancellationToken: cancellationToken, options: new JsonSerializerOptions());        
    }

    /// <summary>
    /// Publishes the specified process point on the broker.
    /// </summary>
    public async Task PublishAsync(ProcessPoint processPoint, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }

    /// <summary>
    /// Publishes the specified process points on the broker.
    /// </summary>
    public async Task PublishAsync(IEnumerable<ProcessPoint> processPoints, CancellationToken cancellationToken = default) { throw new NotImplementedException(); }
}