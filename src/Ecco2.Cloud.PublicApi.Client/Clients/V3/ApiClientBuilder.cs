using System;
using System.Linq;
using System.Net.Http;
using Ecco2.Cloud.PublicApi.Client.V3.Entities;


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
    /// <returns>The reference to the <see cref="ApiClientBuilder"/> for command chaining.</returns>
    public ApiClientBuilder WithCredentials(string clientId, string clientSecret)
    {
        _config.ClientId = clientId;
        _config.ClientSecret = clientSecret;
        
        return this;
    }

    /// <summary>
    /// Uses the specified base address instead of the default one.
    /// </summary>
    /// <param name="baseAddress">The base address to use.</param>
    /// <returns>The reference to the <see cref="ApiClientBuilder"/> for command chaining.</returns>
    public ApiClientBuilder WithDataBaseAddress(string baseAddress)
    {
        if (!String.IsNullOrEmpty(baseAddress)) { _config.BaseAddress = baseAddress; }

        return this;
    }
    
    /// <summary>
    /// Custom configuration for the <seealso cref="HttpMessageHandler"/> that is going to be used by the API client
    /// </summary>
    /// <returns>The reference to the <see cref="ApiClientBuilder"/> for command chaining.</returns>
    public ApiClientBuilder WithConfigureMessageHandler(Func<HttpMessageHandler, HttpMessageHandler> handler)
    {
        _config.ConfigureMessageHandler = handler;

        return this;
    }

    /// <summary>
    /// Builds the API client specified by the type parameter
    /// </summary>
    /// <typeparam name="T">The interface of the API client to create.</typeparam>
    /// <returns>A configured API client.</returns>
    public T Build<T>() where T: class, IApiClient
    {
        if (!IsValidBuilt()) { throw new InvalidOperationException("Builder state is not valid. Make sure you set all required configurations before building a client."); }

        var iType = typeof(T);
        var implementor = this.GetType().Assembly.GetTypes().FirstOrDefault(t => !t.IsNested && !t.IsAbstract && t.GetInterfaces().Contains(iType));

        if (implementor is null) { throw new ArgumentException("Specified API client is not supported by this builder"); }
        if (Activator.CreateInstance(implementor, _config) is not T ci) { throw new ArgumentException("Specified API client is not supported by this builder"); }

        return ci;
    }



    //
    // verifies if the built configuration is valid
    private bool IsValidBuilt()
    {
        return !String.IsNullOrEmpty(_config.ClientId) && !String.IsNullOrEmpty(_config.ClientSecret);
    }
}