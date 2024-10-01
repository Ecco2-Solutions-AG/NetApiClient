using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Ecco2.Cloud.PublicApi.Client.V3.Entities;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
internal class HistorianClient: ApiClientBase, IHistorianClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataBrokerClient "/> class.
    /// </summary>
    public HistorianClient(ApiClientConfiguration clientConfiguration) : base(clientConfiguration)
    {
        HttpClient.BaseAddress = new Uri($"{clientConfiguration.BaseAddress.TrimEnd('/')}/historian/measurements/");
    }
    


    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    public async Task<HistorianData[]> GetSeriesAsync(Guid identifier, HistorianRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (identifier == Guid.Empty) { throw new ArgumentException("Value cannot be null or empty.", nameof(identifier)); }

        var response = await HttpClient.GetAsync($"series?identifier={identifier:D}&{request.ToQueryString()}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var dictionary = await response.Content.ReadFromJsonAsync<Dictionary<Guid, HistorianData[]>>(SerializationOptions.PerformanceWithStringEnum, cancellationToken);
        return dictionary.TryGetValue(identifier, out var series) ? series : [];
    }

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    public async Task<HistorianData[]> GetSeriesAsync(Guid projectIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (projectIdentifier == Guid.Empty) { throw new ArgumentException("Value cannot be null or empty.", nameof(projectIdentifier)); }
        if (String.IsNullOrEmpty(channelCode)) { throw new ArgumentException("Value cannot be null or empty.", nameof(channelCode)); }

        var response = await HttpClient.GetAsync($"series?projectIdentifier={projectIdentifier:D}&channelName={channelCode}&{request.ToQueryString()}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var dictionary = await response.Content.ReadFromJsonAsync<Dictionary<string, HistorianData[]>>(SerializationOptions.PerformanceWithStringEnum, cancellationToken) ?? new Dictionary<string, HistorianData[]>();
        return dictionary.TryGetValue(channelCode, out var series) ? series : [];
    }

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    public async Task<HistorianData[]> GetSeriesAsync(Guid projectIdentifier, Guid elementIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (projectIdentifier == Guid.Empty) { throw new ArgumentException("Value cannot be null or empty.", nameof(projectIdentifier)); }
        if (elementIdentifier == Guid.Empty) { throw new ArgumentException("Value cannot be null or empty.", nameof(elementIdentifier)); }
        if (String.IsNullOrEmpty(channelCode)) { throw new ArgumentException("Value cannot be null or empty.", nameof(channelCode)); }

        var response = await HttpClient.GetAsync($"series?projectIdentifier={projectIdentifier:D}&elementIdentifier={elementIdentifier:D}&channelName={channelCode}&{request.ToQueryString()}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var dictionary = await response.Content.ReadFromJsonAsync<Dictionary<string, HistorianData[]>>(SerializationOptions.PerformanceWithStringEnum, cancellationToken) ?? new Dictionary<string, HistorianData[]>();
        return dictionary.TryGetValue(channelCode, out var series) ? series : [];
    }
}