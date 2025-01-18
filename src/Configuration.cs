namespace Sharpflake;

public class Configuration {

    public int  WorkedId     { get; }
    public long Epoch        { get; }

    // These are according to spec and should not be changed
    internal static int TimestampBits       => 41;
    internal static int MachineIdBits       => 10;
    internal static int MachineSequenceBits => 12;

    /// <summary>
    /// Initializes the Snowflake Generator
    ///
    /// The default epoch timestamp is 1st of June 2010 when Twitter announced Snowflake usage
    /// </summary>
    /// <param name="epoch">Epoch TimeStamp (in milliseconds) to start generating the Snowflakes</param>
    /// <param name="workedId">Worker ID that is going to generate the Snowflakes</param>
    public Configuration(long epoch = 1275350400000, int workedId = 1) {
        WorkedId     = workedId;
        Epoch        = epoch;
    }

    /// <summary>
    /// Initializes the Snowflake Generator
    /// </summary>
    /// <param name="epoch">DateTime where the Worker ID should to start generating timestamps for the Snowflakes</param>
    /// <param name="workedId">Worker ID that is going to generate the Snowflakes</param>
    public Configuration(DateTime epoch, int workedId = 1) {
        WorkedId     = workedId;
        Epoch        = Helpers.GenerateTimestamp(epoch);
    }

}
