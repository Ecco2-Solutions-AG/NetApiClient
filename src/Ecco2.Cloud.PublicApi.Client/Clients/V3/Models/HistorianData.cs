using System;
using System.Text.Json.Serialization;


namespace Ecco2.Cloud.PublicApi.Client.V3;

public class HistorianData
{
    /// <summary>
    /// The globally unique identifier of the measurement
    /// </summary>
    [JsonPropertyName("id")]
    public string Identifier { get; set; }

    /// <summary>
    /// The value of the process point.
    /// </summary>
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    /// <summary>
    /// The quality tag to be applied to the Value.
    /// </summary>
    [JsonPropertyName("quality")]
    public Quality Quality { get; set; }
	
    /// <summary>
    /// The time stamp of the Value, in UTC.
    /// </summary>
    [JsonPropertyName("timeStamp")]
    public DateTime TimeStamp { get; set; }
}