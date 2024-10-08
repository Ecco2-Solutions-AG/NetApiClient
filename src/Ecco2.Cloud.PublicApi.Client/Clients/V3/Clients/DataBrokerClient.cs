﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Ecco2.Cloud.PublicApi.Client.V3.Entities;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
internal class DataBrokerClient: ApiClientBase, IDataBrokerClient
{
    public DataBrokerClient(ApiClientConfiguration clientConfiguration) : base(clientConfiguration)
    {
        HttpClient.BaseAddress = new Uri($"{clientConfiguration.BaseAddress.TrimEnd('/')}/broker/");
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
        
        return await response.Content.ReadFromJsonAsync<ProcessPoint[]>(SerializationOptions.PerformanceWithStringEnum, cancellationToken) ?? Array.Empty<ProcessPoint>();
    }

    /// <summary>
    /// Publishes the specified process point on the broker.
    /// </summary>
    public async Task PublishAsync(ProcessPoint processPoint, CancellationToken cancellationToken = default)
    {
        if (processPoint is null) { throw new ArgumentNullException(nameof(processPoint)); }
        if (processPoint.Identifier == Guid.Empty) { throw new ArgumentException("Identifier cannot be null"); }

        var response = await HttpClient.PutAsJsonAsync("measurements", new[] { processPoint }, SerializationOptions.PerformanceWithStringEnum, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    /// <summary>
    /// Publishes the specified process points on the broker.
    /// </summary>
    public async Task PublishAsync(IEnumerable<ProcessPoint> processPoints, CancellationToken cancellationToken = default)
    {
        if (processPoints is null) { throw new ArgumentNullException(nameof(processPoints)); }

        var response = await HttpClient.PutAsJsonAsync("measurements", processPoints.Where(p => p.Identifier != Guid.Empty), SerializationOptions.PerformanceWithStringEnum, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}