using System;
using System.Net.Http;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// A builder for ECCO2 public API data broker clients
/// </summary>
/// <remarks>
/// The client is intended to be built once and re-used throughout the life of an application.
/// Building a client for every request will exhaust the number of sockets available under heavy loads. This will result in SocketException errors.
/// </remarks>
public sealed class ApiClientBuilder 
{
    private readonly ApiClientConfiguration _config = new ();

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
    public void WithDataBaseAddress(string baseAddress)
    {
        if (!String.IsNullOrEmpty(baseAddress)) { _config.DataBaseAddress = baseAddress; }
    }

    /// <summary>
    /// Uses the specified base address instead of the default one.
    /// </summary>
    /// <param name="baseAddress">The base address to use.</param>
    public void WithAuthBaseAddress(string baseAddress)
    {
        if (!String.IsNullOrEmpty(baseAddress)) { _config.AuthBaseAddress = baseAddress; }
    }
    
    /// <summary>
    /// Custom configuration for the <seealso cref="HttpMessageHandler"/> that is going to be used by the API client
    /// </summary>
    /// <remarks>This handler is going to be used for auth end data endpoints.</remarks>
    public void WithConfigureMessageHandler(Func<HttpMessageHandler, HttpMessageHandler> handler)
    {
        _config.ConfigureMessageHandler = handler;
    }

    /// <summary>
    /// Builds the API client specified by the type parameter
    /// </summary>
    /// <typeparam name="T">The interface of the API client to create.</typeparam>
    /// <returns>A configured API client.</returns>
    public T Build<T>() where T: class, IApiClient
    {
        if (!IsValidBuilt()) { throw new InvalidOperationException("Builder state is not valid. Make sure you set all required configurations before building a client."); }

        var type = typeof(T);
        if (type == typeof(IDataBrokerClient)) { return new DataBrokerClient(_config) as T; }
        if (type == typeof(IHistorianClient)) { return new HistorianClient(_config) as T; }

        throw new ArgumentException("Specified API client is not supported by this builder"); 
    }


    //
    // verifies if the built configuration is valid
    private bool IsValidBuilt()
    {
        return !String.IsNullOrEmpty(_config.ClientId) && !String.IsNullOrEmpty(_config.ClientSecret);
    }
}