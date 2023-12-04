using System;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// A builder for ECCO2 public API data broker clients
/// </summary>
/// <remarks>
/// The client is intended to be built once and re-used throughout the life of an application.
/// Building a client for every request will exhaust the number of sockets available under heavy loads. This will result in SocketException errors.
/// </remarks>
public sealed class DataBrokerClientBuilder
{
    private readonly Ecco2ClientConfiguration _config = new ();


    /// <summary>
    /// Adds the credentials to be used to generate the authentication token.
    /// </summary>
    /// <param name="clientId">The client ID as provided by Ecco2</param>
    /// <param name="clientSecret">Secret of the client. Its value is initially provided by Ecco2 together with the client ID. It may have been updated by the third party since.</param>
    public void WithCredentials(string clientId, string clientSecret)
    {
        _config.ClientId = clientId;
        _config.ClientSecret = clientSecret;
    }

    /// <summary>
    /// Uses the specified base address instead of the default one.
    /// </summary>
    /// <param name="baseAddress">The base address to use.</param>
    public void WithBaseAddress(string baseAddress)
    {
        if (!String.IsNullOrEmpty(baseAddress)) { _config.BaseAddress = baseAddress; }
    }

    /// <summary>
    /// Builds the <see cref="IDataBrokerClient"/>
    /// </summary>
    /// <returns>A configured <see cref="IDataBrokerClient"/>.</returns>
    public IDataBrokerClient Build()
    {
        return new DataBrokerClient(_config);
    }
}