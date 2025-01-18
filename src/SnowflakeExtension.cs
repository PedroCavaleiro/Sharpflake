namespace Sharpflake;

public static class SnowflakeExtension {

    public static Snowflake ToSnowflake(this long snowflake, Configuration? configuration = null) =>
        new(snowflake, configuration ?? new Configuration());

    public static Snowflake ToSnowflake(this string snowflake, Configuration? configuration = null) =>
        new(snowflake, configuration ?? new Configuration());

    public static Snowflake ToSnowflake(this long snowflake, ISnowflakeGenerator generator) =>
        new(snowflake, generator.Configuration);

    public static Snowflake ToSnowflake(this string snowflake, ISnowflakeGenerator generator) =>
        new(snowflake, generator.Configuration);

}
