using System;
using System.Net.Http;


namespace Ecco2.Cloud.PublicApi.Client.V3;

public class ApiClientConfiguration
{
    /// <summary>
    /// The base address to use whe connecting to data endpoints of the public API.
    /// </summary>
    public string DataBaseAddress { get; internal set; } = "https://api.ecco2.ch/api/v3";
    
    /// <summary>
    /// The base address to use whe connecting to authentication endpoints of the public API.
    /// </summary>
    public string AuthBaseAddress { get; internal set; } = "https://api.ecco2.ch/api/v3";

    /// <summary>
    /// The client ID to be used for authentication.
    /// </summary>
    public string ClientId { get; internal set; }

    /// <summary>
    /// The client secret to be used for authentication.
    /// </summary>
    public string ClientSecret { get; internal set; }

    /// <summary>
    /// Custom configuration for the <seealso cref="HttpMessageHandler"/> that is going to be used by the API client
    /// </summary>
    /// <remarks>This handler is going to be used for auth end data endpoints.</remarks>
    public Func<HttpMessageHandler, HttpMessageHandler> ConfigureMessageHandler { get; internal set; }
}