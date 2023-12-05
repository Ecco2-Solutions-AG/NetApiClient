namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Aggregation interval expressed as ISO 8601 duration
/// </summary>
public enum AggregationInterval
{
    /// <summary>
    /// 1 hour interval
    /// </summary>
    PT1H = 2,

    /// <summary>
    /// 1 day interval
    /// </summary>
    P1D = 3,

    /// <summary>
    /// 1 month interval
    /// </summary>
    P1M = 4,

    /// <summary>
    /// 1 year interval
    /// </summary>
    P1Y = 5,
}