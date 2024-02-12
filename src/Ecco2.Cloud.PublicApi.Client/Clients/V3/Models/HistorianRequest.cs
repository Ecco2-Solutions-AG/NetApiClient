using System;
using System.Text.Json.Serialization;


namespace Ecco2.Cloud.PublicApi.Client.V3;

public class HistorianRequest
{
	/// <summary>
	/// The start date of the period of interest in ISO8601 date format (UTC, included).
    /// Falls back to the server's defaults if not specified.
	/// </summary>
	[JsonPropertyName("from"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? From { get; set; }

	/// <summary>
	/// The end date of the period of interest in ISO8601 date format (UTC, excluded).
	/// Falls back to the server's defaults if not specified.
	/// </summary>
    [JsonPropertyName("to"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public DateTime? To { get; set; }

	/// <summary>
	/// The aggregation interval to apply.
    /// Falls back to the server's defaults if not specified.
	/// </summary>
    [JsonPropertyName("interval")]
	public AggregationInterval AggregationInterval { get; set; }
}