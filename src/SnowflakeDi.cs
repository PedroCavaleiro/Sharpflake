using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sharpflake;

public static class SnowflakeDi {

    public static IServiceCollection AddSnowflakeIdGenerator(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSnowflakeIdGenerator(new Configuration());

    public static IServiceCollection AddSnowflakeIdGenerator(
        this IServiceCollection serviceCollection,
        DateTime                customEpoch,
        int                     machineId
    ) => serviceCollection.AddSnowflakeIdGenerator(new Configuration(customEpoch, machineId));

    public static IServiceCollection AddSnowflakeIdGenerator(
        this IServiceCollection serviceCollection,
        int                     machineId
    ) => serviceCollection.AddSnowflakeIdGenerator(new Configuration(workedId: machineId));

    public static IServiceCollection AddSnowflakeIdGenerator(
        this IServiceCollection serviceCollection,
        Configuration          configuration
    ) {
        serviceCollection.TryAddSingleton<ISnowflakeGenerator>(
            sp => new SnowflakeGenerator(
                configuration
            )
        );

        return serviceCollection;
    }
}
