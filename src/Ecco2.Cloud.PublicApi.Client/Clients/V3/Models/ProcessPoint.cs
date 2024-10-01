using System;
using System.Text.Json.Serialization;


namespace Ecco2.Cloud.PublicApi.Client.V3;

/// <summary>
/// Represents a data point on the ECCO2 data broker.
/// </summary>
public class ProcessPoint
{
	/// <summary>
	/// The agreed-upon, globally unique identifier of the process point.
	/// </summary>
	[JsonPropertyName("id")]
	public Guid Identifier { get; set; }

	/// <summary>
	/// The value of the process point.
	/// </summary>
	[JsonPropertyName("value")]
	public double? Value { get; set; }

	/// <summary>
	/// The quality tag to be applied to the value.
	/// </summary>
	[JsonPropertyName("quality")]
	public Quality Quality { get; set; }
	
	/// <summary>
	/// The time stamp of the value, in UTC.
	/// </summary>
	[JsonPropertyName("timeStamp")]
	public DateTime TimeStamp { get; set; }
}