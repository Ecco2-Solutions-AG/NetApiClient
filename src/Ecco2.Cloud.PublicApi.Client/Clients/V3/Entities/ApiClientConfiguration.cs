using System;
using System.Net.Http;


namespace Ecco2.Cloud.PublicApi.Client.V3.Entities;

internal class ApiClientConfiguration
{
    /// <summary>
    /// The base address to use when connecting to the endpoints of the public API.
    /// </summary>
    public string BaseAddress { get; internal set; } = "https://api.ecco2.ch/api/v3";
    
    /// <summary>
    /// The client ID to be used for authentication.
    /// </summary>
    public string ClientId { get; internal set; }

    /// <summary>
    /// The client secret to be used for authentication.
    /// </summary>
    public string ClientSecret { get; internal set; }

    /// <summary>
    /// Custom configuration for the <seealso cref="HttpMessageHandler"/> that is going to be used by the API client.
    /// </summary>
    public Func<HttpMessageHandler, HttpMessageHandler> ConfigureMessageHandler { get; internal set; }
}