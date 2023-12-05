namespace Ecco2.Cloud.PublicApi.Client.V3;

public static class QualityExtensions
{
    /// <summary>
    /// Determines whether the major value of this quality is good.
    /// </summary>
    public static bool IsGood(this Quality q) { return q >= Quality.Good; }

    /// <summary>
    /// Determines whether the major value of this quality is not good.
    /// </summary>
    public static bool IsNotGood(this Quality q) { return q < Quality.Good; }

    /// <summary>
    /// Determines whether the major value of this quality is bad.
    /// </summary>
    public static bool IsBad(this Quality q) { return q < Quality.Uncertain; }

    /// <summary>
    /// Determines whether the major value of this quality is not bad.
    /// </summary>
    public static bool IsNotBad(this Quality q) { return q >= Quality.Uncertain; }

    /// <summary>
    /// Determines whether the major value of this quality is uncertain.
    /// </summary>
    public static bool IsUncertain(this Quality q) { return q is >= Quality.Uncertain and < Quality.Good; }
}