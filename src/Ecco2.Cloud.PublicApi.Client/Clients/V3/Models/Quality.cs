namespace Ecco2.Cloud.PublicApi.Client.V3;


/// <summary>
/// The quality enumeration allows to quantify the quality of a measure
/// </summary>
/// <remarks>
/// Ecco2 uses specific quality based on a major quality value and a sub-status for that major quality value.
/// The major quality values are: <code>Good</code> (generally indicates the data is valid),
/// <code>Uncertain</code> (generally indicates the data is speculative in some manner) and <code>Bad</code> (generally indicates the data is not valid).
/// The sub-status gives a reason for the major quality
/// </remarks>
public enum Quality
{
    /// <summary>
    /// The value is good. There are no special conditions.
    /// </summary>
    GoodNonSpecific = 0x800000,

    /// <summary>
    /// The value is good but the value has been overridden.
    /// Typically, this means that a manually entered value has been forced.
    /// </summary>
    GoodLocalOverwrite  = 0x809600,

    /// <summary>
    /// The value is good but the value has been substituted.
    /// Typically, this means that some local algorithm has forced the value.
    /// </summary>
    GoodSubstitute = 0x809100,

    /// <summary>
    /// The value is good but the value has been computed.
    /// Typically, this means that some local algorithm has computed the value.
    /// </summary>
    GoodComputed = 0x809200,





    /// <summary>
    /// The quality of the value is uncertain, no reason why has been given by the source
    /// </summary>
    UncertainNonSpecific = 0x400000,

    /// <summary>
    /// The quality of the value is uncertain.
    /// Whatever was writing the data value has stopped doing so. The returned value should be regarded as "stale." 
    /// </summary>
    /// <remarks>You can examine the age of the value using the <code>TimeStamp</code> property associated with this quality.</remarks>
    UncertainLastUsableValue = 0x409000,

    /// <summary>
    /// The value is uncertain as it has been substituted.
    /// Typically, this means that an algorithm has forced the value and is uncertain about the computed value.
    /// For example because the value is based on uncertain sources or it is at its startup default value.
    /// </summary>
    UncertainSubstitute = 0x409100,

    /// <summary>
    /// The quality of the value is uncertain.
    /// Either the value has pegged at one of the sensor limits, or the sensor is otherwise
    /// known to be out of calibration via some form of internal diagnostics
    /// </summary>
    UncertainSensorNotAccurate = 0x409300,

    /// <summary>
    /// The quality of the value is uncertain.
    /// The local algorithm tried to compute or substitute the value but could not determine the value to a sufficient degree of precision.
    /// </summary>
    UncertainComputationNotAccurate = 0x409350,

    /// <summary>
    /// The quality of the value is uncertain.
    /// The local algorithm tried to compute or substitute the value but could not determine the value to a sufficient degree of precision.
    /// </summary>
    UncertainSubstituteNotAccurate = 0x409351,

    /// <summary>
    /// The quality of the value is uncertain.
    /// The returned value is outside the limits defined for this value.
    /// </summary>
    UncertainEngineeringUnitsExceeded = 0x409400,

    /// <summary>
    /// The quality of the value is uncertain.
    /// The value is derived from multiple sources and has less than the required number of good sources.
    /// </summary>
    UncertainSubNormal = 0x409500,

    /// <summary>
    /// The value is uncertain but the value has been overridden.
    /// Typically, this means that a manually entered value has been forced.
    /// </summary>
    UncertainLocalOverwrite = 0x409600,





    /// <summary>
    /// The value is bad (not useful) but no specific reason is known.
    /// </summary>
    BadNonSpecific = 0x000000,

    /// <summary>
    /// The value is bad (not useful).
    /// There is some source-specific problem with the configuration.
    /// For example, the item in question is deleted from the running server configuration.
    /// </summary>
    BadConfigurationError = 0x001000,

    /// <summary>
    /// The value is bad (not useful).
    /// The input is required to be logically connected to something, but is not connected.
    /// This quality may reflect that no value is available at this time, possibly because the data source has not yet provided one
    /// </summary>
    /// <remarks>This is considered to be the default value for the quality flag.</remarks>
    BadNotConnected = 0x00D000,
        
    /// <summary>
    /// The value is bad (not useful).
    /// Typically, this means that some local algorithm tried to compute or substitute the value but failed to do so.
    /// </summary>
    BadValueNotComputed = 0x00E000,

    /// <summary>
    /// The value is bad (not useful).
    /// Typically, this means that some specific value has been searched for but could not be found.
    /// </summary>
    BadValueNotFound = 0x00E100,

    /// <summary>
    /// The value is bad (not useful) and does not represent any value.
    /// Typically it comes from an aggregation computed without any value (an empty buffer for instance).
    /// </summary>
    BadNoValue = 0x00E200,

    /// <summary>
    /// The value is bad (not useful).
    /// A device failure has been detected.
    /// </summary>
    BadDeviceFailure = 0x008B00,

    /// <summary>
    /// The value is bad (not useful).
    /// A sensor failure has been detected.
    /// </summary>
    BadSensorFailure = 0x008C00,

    /// <summary>
    /// The value is bad (not useful).
    /// A sensor failure has been detected probably due to a by-pass system.
    /// </summary>
    BadSensorByPassed = 0x008D00,

    /// <summary>
    /// The value is bad (not useful).
    /// A sensor failure has been detected probably due to a measurement that is outside of the sensor limit.
    /// </summary>
    BadOutOfSensorLimits = 0x008E00,

    /// <summary>
    /// The value is bad (not useful).
    /// The communication between the device and server has failed. There is no last usable value available.
    /// </summary>
    BadCommunicationError = 0x005000,

    /// <summary>
    /// The value is bad and the value has been overridden.
    /// Typically, this means that a manually entered value has been forced.
    /// </summary>
    BadLocalOverwrite = 0x009600,

    /// <summary>
    /// The value is bad (not useful).
    /// The Active state of the item or group containing the item is set to off. 
    /// </summary>
    BadOutOfService = 0x00BF00,

    /// <summary>
    /// The value is bad (not useful).
    /// The object cannot be read 
    /// </summary>
    BadObjectNotReadable = 0x003A00,

    /// <summary>
    /// The value is bad (not useful).
    /// The object cannot be written
    /// </summary>
    BadObjectNotWriteable = 0x003B00,

    /// <summary>
    /// The value is bad (not useful).
    /// The object has not been found on the server
    /// </summary>
    BadObjectNotFound = 0x003E00,

    /// <summary>
    /// The value is bad (not useful).
    /// The value cannot be accessed as the object has been marked for deletion
    /// </summary>
    BadObjectDeleted = 0x003F00,




    // some useful shortcuts
    Good = GoodNonSpecific,
    Bad = BadNonSpecific,
    Uncertain = UncertainNonSpecific,
}