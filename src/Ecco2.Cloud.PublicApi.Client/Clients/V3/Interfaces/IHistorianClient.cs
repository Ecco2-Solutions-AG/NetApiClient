using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Historian endpoints of the Ecco2 public API
/// </summary>
public interface IHistorianClient: IApiClient
{
    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    /// <param name="projectIdentifier">The globally unique identifier of the installation site</param>
    /// <param name="channelCode">The measurement channel's code</param>
    /// <param name="request">The data request object</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A collection of time series grouped by channel name; empty collection if none found</returns>
    /// <remarks>
    /// For a complete list of available measurements refer to the API documentation.
    /// </remarks>
    Task<Dictionary<string, HistorianData[]>> GetSeriesAsync(string projectIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    /// <param name="projectIdentifier">The globally unique identifier of the installation site.</param>
    /// <param name="elementIdentifier">The globally unique identifier of the grouping element.</param>
    /// <param name="channelCode">The measurement channel's code.</param>
    /// <param name="request">The data request object</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A collection of time series grouped by channel name; empty collection if none found</returns>
    /// <remarks>
    /// For a complete list of available measurements refer to the API documentation.
    /// The grouping element can be an entrance, a heating group, or a sensor location.
    /// Refer to the configuration data structure of the installation site to retrieve these identifiers.
    /// </remarks>
    Task<Dictionary<string, HistorianData[]>> GetSeriesAsync(string projectIdentifier, string elementIdentifier, string channelCode, HistorianRequest request, CancellationToken cancellationToken = default);
}