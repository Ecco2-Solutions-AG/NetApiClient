using System;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Historian endpoints of the Ecco2 public API
/// </summary>
public interface IHistorianClient: IApiClient
{
    /// <summary>
    /// Authenticates the client.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task AuthenticateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution.
    /// </summary>
    /// <param name="identifier">The globally unique identifier of the measurement.</param>
    /// <param name="request">The data request object.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A collection of time series; empty collection if none found.</returns>
    Task<HistorianData[]> GetSeriesAsync(Guid identifier, HistorianRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution.
    /// </summary>
    /// <param name="projectIdentifier">The globally unique identifier of the installation site.</param>
    /// <param name="channelCode">The measurement channel's code.</param>
    /// <param name="request">The data request object.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A collection of time series; empty collection if none found.</returns>
    /// <remarks>
    /// For a complete list of available measurements refer to the API documentation.
    /// </remarks>
    Task<HistorianData[]> GetSeriesAsync(Guid projectIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution.
    /// </summary>
    /// <param name="projectIdentifier">The globally unique identifier of the installation site.</param>
    /// <param name="elementIdentifier">The globally unique identifier of the grouping element.</param>
    /// <param name="channelCode">The measurement channel's code.</param>
    /// <param name="request">The data request object</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A collection of time series; empty collection if none found.</returns>
    /// <remarks>
    /// For a complete list of available measurements refer to the API documentation.
    /// The grouping element can be an entrance, a heating group, or a sensor location.
    /// Refer to the configuration data structure of the installation site to retrieve these identifiers.
    /// </remarks>
    Task<HistorianData[]> GetSeriesAsync(Guid projectIdentifier, Guid elementIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default);
}