using System;
using System.Linq;


namespace Ecco2.Cloud.PublicApi.Client.V3;

internal static class HistorianRequestExtensions
{
    public static string ToQueryString(this HistorianRequest request)
    {
        return String.Join("&", new[]
        {
            request.From is null ? null : $"from={request.From.Value:O}",
            request.To is null ? null : $"to={request.To.Value:O}",
            request.AggregationInterval is null ? null : $"interval={request.AggregationInterval.Value:G}",
        }.Where(q => q is not null));
    }
}