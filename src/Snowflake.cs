// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sharpflake;

public class Snowflake {
    public long SnowflakeId { get; }

    public long Timestamp { get; private set; }
    public int  MachineId { get; private set; }
    public int  Sequence  { get; private set; }

    private readonly long _maskTime      = Helpers.GetMask(41);
    private readonly long _maskGenerator = Helpers.GetMask(10);
    private readonly long _maskSequence  = Helpers.GetMask(12);

    private const int ShiftTime = 10 + 12;

    public DateTime Time => TimestampToDateTime(Timestamp);

    private Configuration _configuration;

    /// <summary>
    /// Updates the configuration to get the proper timestamps
    /// </summary>
    /// <param name="configuration">Configuration used to generate this snowflake</param>
    public void UpdateConfiguration(Configuration configuration) => _configuration = configuration;

    #region Init

    public Snowflake(long snowflake, Configuration configuration) {
        _configuration = configuration;
        SnowflakeId    = snowflake;
        DecodeSnowflake(snowflake);
    }

    public Snowflake(long snowflake) {
        _configuration = new Configuration();
        SnowflakeId    = snowflake;
        DecodeSnowflake(snowflake);
    }

    public Snowflake(string snowflake, Configuration configuration) {
        _configuration = configuration;
        SnowflakeId    = long.Parse(snowflake);
        DecodeSnowflake(long.Parse(snowflake));
    }

    public Snowflake(string snowflake) {
        _configuration = new Configuration();
        SnowflakeId    = long.Parse(snowflake);
        DecodeSnowflake(long.Parse(snowflake));
    }

    #endregion

    public static explicit operator Snowflake(string snowflake) => new(snowflake);
    public static explicit operator Snowflake(long   snowflake) => new(snowflake);

    private void DecodeSnowflake(long snowflake) {
        Timestamp = (snowflake >> ShiftTime) + _configuration.Epoch;
        MachineId = (int)((snowflake >> Configuration.MachineSequenceBits) & _maskGenerator);
        Sequence  = (int)(snowflake & _maskSequence);
    }

    private DateTime TimestampToDateTime(long timestamp) {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0,
                                    DateTimeKind.Utc);
        dateTime = dateTime
                   .AddMilliseconds(timestamp)
                   .ToLocalTime();
        return dateTime;
    }
}