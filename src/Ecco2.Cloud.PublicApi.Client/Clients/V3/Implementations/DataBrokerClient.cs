using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
internal class DataBrokerClient: ApiClientBase, IDataBrokerClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataBrokerClient "/> class.
    /// </summary>
    public DataBrokerClient(ApiClientConfiguration clientConfiguration) : base(clientConfiguration)
    {
        HttpClient.BaseAddress = new Uri($"{clientConfiguration.DataBaseAddress.TrimEnd('/')}/broker/");
    }


    
    /// <summary>
    /// Authenticates the client.
    /// </summary>
    public virtual async Task<JwtToken> AuthenticateAsync(CancellationToken cancellationToken = default)
    {
        var token = await new AuthenticationClient(ClientConfiguration).AuthenticateAsync(ClientConfiguration, cancellationToken);
        HttpClient.DefaultRequestHeaders.Add("Authorization", $"{token.TokenType} {token.Token}");

        return token;
    }
    
    /// <summary>
    /// Requests the process point stored at the specified identifier.
    /// </summary>
    public async Task<ProcessPoint> GetAsync(Guid identifier, CancellationToken cancellationToken = default)
    {
        if (identifier == Guid.Empty) { throw new ArgumentException("Identifier must be specified"); }

        var response = await HttpClient.GetAsync($"measurements/{identifier:D}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ProcessPoint>(SerializationOptions.PerformanceWithStringEnum, cancellationToken);
    }

    /// <summary>
    /// Requests the process points stored at the specified identifiers.
    /// </summary>
    public async Task<ProcessPoint[]> GetAsync(IEnumerable<Guid> identifiers, CancellationToken cancellationToken = default)
    {
        if (identifiers is null) { throw new ArgumentNullException(nameof(identifiers)); }

        var response = await HttpClient.PostAsJsonAsync("measurements/get-range", identifiers.Where(i => i != Guid.Empty), cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ProcessPoint[]>(SerializationOptions.PerformanceWithStringEnum, cancellationToken);
    }

    /// <summary>
    /// Publishes the specified process point on the broker.
    /// </summary>
    public async Task PublishAsync(ProcessPoint processPoint, CancellationToken cancellationToken = default)
    {
        if (processPoint is null) { throw new ArgumentNullException(nameof(processPoint)); }
        if (String.IsNullOrEmpty(processPoint.Identifier)) { throw new ArgumentException("Identifier cannot be null"); }

        var response = await HttpClient.PutAsJsonAsync("measurements", processPoint, SerializationOptions.PerformanceWithStringEnum, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Publishes the specified process points on the broker.
    /// </summary>
    public async Task PublishAsync(IEnumerable<ProcessPoint> processPoints, CancellationToken cancellationToken = default)
    {
        if (processPoints is null) { throw new ArgumentNullException(nameof(processPoints)); }

        var response = await HttpClient.PutAsJsonAsync("measurements/put-range", processPoints.Where(p => !String.IsNullOrEmpty(p.Identifier)), SerializationOptions.PerformanceWithStringEnum, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}