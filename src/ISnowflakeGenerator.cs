namespace Sharpflake;

public interface ISnowflakeGenerator {

    public Configuration Configuration { get; }
    public long          GenerateSnowflake();

}
