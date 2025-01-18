namespace Sharpflake;

public class SnowflakeGenerator(Configuration configuration): ISnowflakeGenerator {

    private readonly int _maxSequence;

    private long _lastTimestamp    = -1;
    private long _sequence;

    private const int ShiftTime = 10 + 12;
    private const int ShiftGenerator = 12;

    public Configuration Configuration { get; } = configuration;

    /// <summary>
    /// Initialization with the default configuration
    /// </summary>
    public SnowflakeGenerator() : this(new Configuration()) {
        _maxSequence = (1 << Configuration.MachineSequenceBits) - 1;

        if (Configuration.Epoch > Helpers.GenerateTimestamp(DateTime.UtcNow))
            throw new Exception(
                $"Invalid epoch: {Configuration.Epoch}. It can't be greater than the current timestamp!"
            );
    }

    /// <summary>
    /// Generates the Snowflake ID
    /// </summary>
    /// <returns>The Snowflake</returns>
    public long GenerateSnowflake() {
        var timestamp = Helpers.GenerateTimestamp(DateTime.UtcNow);

        if (timestamp < _lastTimestamp)
            throw new Exception("Clock is moving backwards!");

        if (timestamp == _lastTimestamp) {
            _sequence = (_sequence + 1) & _maxSequence;
            if (_sequence == 0)
                timestamp = Helpers.WaitUntilNextTimestamp(timestamp);
        } else
            _sequence = 0;

        _lastTimestamp = timestamp;

        return ((timestamp - Configuration.Epoch) << ShiftTime)
               | (uint)(Configuration.WorkedId << ShiftGenerator)
               | _sequence;
    }



}
