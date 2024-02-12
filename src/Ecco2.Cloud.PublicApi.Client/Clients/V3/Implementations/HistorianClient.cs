using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;


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
    public async Task<HistorianData[]> GetSeriesAsync(string identifier, HistorianRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (String.IsNullOrEmpty(identifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(identifier)); }

        var response = await HttpClient.GetAsync($"series?identifier={identifier}&{request.ToQueryString()}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        var dictionary = await response.Content.ReadFromJsonAsync<Dictionary<string, HistorianData[]>>(SerializationOptions.PerformanceWithStringEnum, cancellationToken);
        return dictionary.TryGetValue(identifier, out var series) ? series : Array.Empty<HistorianData>();
    }

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    public async Task<Dictionary<string, HistorianData[]>> GetSeriesAsync(string projectIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (String.IsNullOrEmpty(projectIdentifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(projectIdentifier)); }
        if (String.IsNullOrEmpty(channelCode)) { throw new ArgumentException("Value cannot be null or empty.", nameof(channelCode)); }

        var response = await HttpClient.GetAsync($"series?projectIdentifier={projectIdentifier}&channelName={channelCode}&{request.ToQueryString()}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Dictionary<string, HistorianData[]>>(SerializationOptions.PerformanceWithStringEnum, cancellationToken) ?? new Dictionary<string, HistorianData[]>();
    }

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    public async Task<Dictionary<string, HistorianData[]>> GetSeriesAsync(string projectIdentifier, string elementIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (String.IsNullOrEmpty(projectIdentifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(projectIdentifier)); }
        if (String.IsNullOrEmpty(elementIdentifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(elementIdentifier)); }
        if (String.IsNullOrEmpty(channelCode)) { throw new ArgumentException("Value cannot be null or empty.", nameof(channelCode)); }

        var response = await HttpClient.GetAsync($"series?projectIdentifier={projectIdentifier}&elementIdentifier={elementIdentifier}&channelName={channelCode}&{request.ToQueryString()}", cancellationToken);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<Dictionary<string, HistorianData[]>>(SerializationOptions.PerformanceWithStringEnum, cancellationToken) ?? new Dictionary<string, HistorianData[]>();        
    }
}