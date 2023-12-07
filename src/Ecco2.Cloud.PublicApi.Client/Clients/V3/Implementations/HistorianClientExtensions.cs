using System;
using System.Collections.Generic;


namespace Ecco2.Cloud.PublicApi.Client.V3;

public static class HistorianClientExtensions
{
    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    /// <param name="identifier">The globally unique identifier of the measurement</param>
    /// <param name="request">The data request object</param>
    /// <returns>A collection of time series grouped by channel name; empty collection if none found</returns>
    public static HistorianData[] GetSeries(this IHistorianClient c, string identifier, HistorianRequest request)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (String.IsNullOrEmpty(identifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(identifier)); }

        return AsyncHelper.RunSync(() => c.GetSeriesAsync(identifier, request));
    }

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    /// <param name="projectIdentifier">The globally unique identifier of the installation site</param>
    /// <param name="channelCode">The measurement channel's code</param>
    /// <param name="request">The data request object</param>
    /// <returns>A collection of time series grouped by channel name; empty collection if none found</returns>
    /// <remarks>
    /// For a complete list of available measurements refer to the API documentation.
    /// </remarks>
    public static Dictionary<string, HistorianData[]> GetSeries(this IHistorianClient c, string projectIdentifier, string channelCode, HistorianRequest request)
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (String.IsNullOrEmpty(projectIdentifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(projectIdentifier)); }
        if (String.IsNullOrEmpty(channelCode)) { throw new ArgumentException("Value cannot be null or empty.", nameof(channelCode)); }

        return AsyncHelper.RunSync(() => c.GetSeriesAsync(projectIdentifier, channelCode, request));
    }

    /// <summary>
    /// Gets the time series for the specified measurement, period of interest, and resolution
    /// </summary>
    /// <param name="projectIdentifier">The globally unique identifier of the installation site.</param>
    /// <param name="elementIdentifier">The globally unique identifier of the grouping element.</param>
    /// <param name="channelCode">The measurement channel's code.</param>
    /// <param name="request">The data request object</param>
    /// <returns>A collection of time series grouped by channel name; empty collection if none found</returns>
    /// <remarks>
    /// For a complete list of available measurements refer to the API documentation.
    /// The grouping element can be an entrance, a heating group, or a sensor location.
    /// Refer to the configuration data structure of the installation site to retrieve these identifiers.
    /// </remarks>
    public static Dictionary<string, HistorianData[]> GetSeries(this IHistorianClient c, string projectIdentifier, string elementIdentifier, string channelCode, HistorianRequest request) 
    {
        if (request is null) { throw new ArgumentNullException(nameof(request)); }
        if (String.IsNullOrEmpty(projectIdentifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(projectIdentifier)); }
        if (String.IsNullOrEmpty(elementIdentifier)) { throw new ArgumentException("Value cannot be null or empty.", nameof(elementIdentifier)); }
        if (String.IsNullOrEmpty(channelCode)) { throw new ArgumentException("Value cannot be null or empty.", nameof(channelCode)); }

        return AsyncHelper.RunSync(() => c.GetSeriesAsync(projectIdentifier, elementIdentifier, channelCode, request));
    }
}