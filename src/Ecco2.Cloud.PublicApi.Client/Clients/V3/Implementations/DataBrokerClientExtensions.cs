using System;
using System.Collections.Generic;


namespace Ecco2.Cloud.PublicApi.Client.V3;

public static class DataBrokerClientExtensions
{
    /// <summary>
    /// Requests the process point stored at the specified identifier.
    /// </summary>
    /// <param name="identifier">The globally unique identifier of the process point to query.</param>
    /// <returns>The process point as requested.</returns>
    public static ProcessPoint Get(this IDataBrokerClient c, Guid identifier)
    {
        if (identifier == Guid.Empty) { throw new ArgumentException("Identifier must be specified"); }

        return AsyncHelper.RunSync(() => c.GetAsync(identifier));
    }

    /// <summary>
    /// Requests the process points stored at the specified identifiers.
    /// </summary>
    /// <param name="identifiers">The globally unique identifier of the process points to query.</param>
    /// <returns>The process point as requested.</returns>
    /// <remarks>This endpoint fails if the access to any of the specified entries is unauthorized.</remarks>
    public static ProcessPoint[] Get(this IDataBrokerClient c, IEnumerable<Guid> identifiers)
    {
        if (identifiers is null) { throw new ArgumentNullException(nameof(identifiers)); }

        return AsyncHelper.RunSync(() => c.GetAsync(identifiers));
    }

    /// <summary>
    /// Publishes the specified process point on the broker.
    /// </summary>
    /// <param name="processPoint">The process point to publish</param>
    public static void Publish(this IDataBrokerClient c, ProcessPoint processPoint)
    {
        if (processPoint is null) { throw new ArgumentNullException(nameof(processPoint)); }
        if (String.IsNullOrEmpty(processPoint.Identifier)) { throw new ArgumentException("Identifier cannot be null"); }

        AsyncHelper.RunSync(() => c.PublishAsync(processPoint));
    }

    /// <summary>
    /// Publishes the specified process points on the broker.
    /// </summary>
    /// <param name="processPoints">The process points to publish</param>
    public static void Publish(this IDataBrokerClient c, IEnumerable<ProcessPoint> processPoints)
    {
        if (processPoints is null) { throw new ArgumentNullException(nameof(processPoints)); }

        AsyncHelper.RunSync(() => c.PublishAsync(processPoints));
    }

    /// <summary>
    /// Publishes the specified value on the specified address on the broker.
    /// </summary>
    /// <param name="identifier">The globally unique identifier of the process point to write to.</param>
    /// <param name="value">The value to write.</param>
    /// <remarks>
    /// The quality is set to <see cref="Quality.GoodNonSpecific"/> and the timestamp to <see cref="DateTime.UtcNow"/>
    /// </remarks>
    public static void PublishAsync(this IDataBrokerClient c, Guid identifier, double value)
    {
        if (identifier == Guid.Empty) { throw new ArgumentException("Identifier cannot be null"); }

        AsyncHelper.RunSync(() => c.PublishAsync(new ProcessPoint
        {
            Identifier = identifier.ToString("D"),
            Value = value,
            Quality = Quality.GoodNonSpecific,
            TimeStamp = DateTime.UtcNow
        }));
    }
}