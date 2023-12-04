namespace Ecco2.Cloud.PublicApi.Client.V3;

internal class Ecco2ClientConfiguration
{
    /// <summary>
    /// The base address to use whe connecting to an endpoint of the public API.
    /// </summary>
    public string BaseAddress { get; set; } = "https://api.ecco2.ch/api/v3";
    
    /// <summary>
    /// The client ID to be used for authentication.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// The client secret to be used for authentication.
    /// </summary>
    public string ClientSecret { get; set; }
}