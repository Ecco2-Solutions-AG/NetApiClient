using System.Text.Json.Serialization;


namespace Ecco2.Cloud.PublicApi.Client.V3.Entities;

internal class JwtToken
{
    /// <summary>
    /// The authentication token to be used by the client for calls to the end points of the Ecco2 API.
    /// </summary>
    /// <remarks>
    /// The client must send this token in the Authorization header when making requests to protected resources:
    /// <code>Authorization: {tokenType} {token}</code>
    /// </remarks>
    [JsonPropertyName("token")]
    public string Token { get; set; }

    /// <summary>
    /// Type of the token.
    /// </summary>
    /// <remarks>
    /// The client must send the token type in the Authorization header when making requests to protected resources:
    /// <code>Authorization: {tokenType} {token}</code>
    /// </remarks>
    [JsonPropertyName("tokenType")]
    public string TokenType { get; set; }

    /// <summary>
    /// Time before expiry of the token (in seconds).
    /// </summary>
    [JsonPropertyName("expiresIn")]
    public uint ExpiresIn { get; set; }
}