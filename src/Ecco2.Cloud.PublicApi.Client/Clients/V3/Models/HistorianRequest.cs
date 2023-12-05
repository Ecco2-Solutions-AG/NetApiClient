using System;


namespace Ecco2.Cloud.PublicApi.Client.V3;

public class HistorianRequest
{
	/// <summary>
	/// The start date of the period of interest in ISO8601 date format (UTC, included).
	/// Will fall back to the default period of interest if not specified.
	/// </summary>
	public DateTime? From { get; set; }

	/// <summary>
	/// The end date of the period of interest in ISO8601 date format (UTC, excluded).
	/// Will fall back to the current time if not specified.
	/// </summary>
	public DateTime? To { get; set; }

	/// <summary>
	/// The aggregation interval to apply.
	/// Falls back to monthly resolution if not specified.
	/// </summary>
	public AggregationInterval AggregationInterval { get; set; }
}