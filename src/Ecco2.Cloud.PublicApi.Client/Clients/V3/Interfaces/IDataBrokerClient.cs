using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Provides methods to access the Ecco2 DataBroker via the Ecco2 public API
/// </summary>
public interface IDataBrokerClient
{
    /// <summary>
    /// Authenticates the client.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The JWT token.</returns>
    Task<JwtToken> AuthenticateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Requests the process point stored at the specified identifier.
    /// </summary>
    /// <param name="identifier">The globally unique identifier of the process point to query.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The process point as requested.</returns>
    Task<ProcessPoint> GetAsync(Guid identifier, CancellationToken cancellationToken = default);

    /// <summary>
    /// Requests the process points stored at the specified identifiers.
    /// </summary>
    /// <param name="identifiers">The globally unique identifier of the process points to query.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The process point as requested.</returns>
    /// <remarks>This endpoint fails if the access to any of the specified entries is unauthorized.</remarks>
    Task<ProcessPoint[]> GetAsync(IEnumerable<Guid> identifiers, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes the specified process point on the broker.
    /// </summary>
    /// <param name="processPoint">The process point to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task PublishAsync(ProcessPoint processPoint, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes the specified process points on the broker.
    /// </summary>
    /// <param name="processPoints">The process points to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    Task PublishAsync(IEnumerable<ProcessPoint> processPoints, CancellationToken cancellationToken = default);
}