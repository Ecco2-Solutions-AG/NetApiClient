using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Ecco2.Cloud.PublicApi.Client;

internal static class SerializationOptions
{
    static SerializationOptions()
    {
        Flexible = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Converters = { new JsonStringEnumConverter() }
        };

        Performance = new JsonSerializerOptions();

        PerformanceWithStringEnum = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
	        Converters = { new JsonStringEnumConverter() }
        };
    }

    /// <summary>
    /// Provides a setting for flexible JSON serialization
    /// </summary>
    public static JsonSerializerOptions Flexible { get; }

    /// <summary>
    /// Provides a setting for JSON serialization looking for performance
    /// </summary>
    public static JsonSerializerOptions Performance { get; }
    
    /// <summary>
    /// Provides a setting for JSON serialization looking for performance, but handling enums as strings
    /// </summary>
    public static JsonSerializerOptions PerformanceWithStringEnum { get; }
}