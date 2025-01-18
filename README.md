# Sharpflake
[![Build](https://github.com/PedroCavaleiro/Sharpflake/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/PedroCavaleiro/Sharpflake/actions/workflows/build.yml)
[![Release](https://github.com/PedroCavaleiro/Sharpflake/actions/workflows/release.yml/badge.svg?branch=main)](https://github.com/PedroCavaleiro/Sharpflake/actions/workflows/release.yml)

A Simple C# Snowflake ID library

This will allow you to generate unique IDs for your application but also parse the details back.

## Installation

To install the package, you can use the following command:

```bash
dotnet add package Sharpflake
```
## Usage with Dependency Injection

To use the library together with ASP.net on your Program.cs, while configuring the services, you can add the following:

```csharp
builder.Services.AddSnowflakeIdGenerator();
```

Then you can inject the `ISnowflakeIdGenerator` interface into your services and use it to generate IDs.

```csharp
public class MyController(ISnowflakeIdGenerator generator): ControllerBase {    

    [HttpGet]
    public void MyGet() {
         var snowflake = generator.GenerateSnowflake();
    }
}
```

## Usage without Dependency Injection

If you are not using Dependency Injection, you can create an instance of the `SnowflakeGenerator` class and use it to generate IDs.

```csharp
var generator = new SnowflakeGenerator();
var snowflake = generator.GenerateSnowflake();
```

## Advanced Configuration

You can also configure the generator to use a custom epoch and worker ID,
the epoch can be set using a timestamp in milliseconds or using the DateTime

```csharp
// Using the DateTime class
var configA = new Configuration(
    new DateTime(2024, 7, 19, 0, 0, 0, 0),
    0
);

// Using a timestamp
var configB = new Configuration(
    1577836800000,
    1
);

var generatorA = new SnowflakeGenerator(configA);
var generator2 = new SnowflakeGenerator(configB);
```

When using a dependency injection, this can be done by adding the configuration to the `AddSnowflakeIdGenerator` method.

```csharp
// Using the DateTime class
builder.Services.AddSnowflakeIdGenerator(new SnowflakeConfig(
    new DateTime(2024, 7, 19, 0, 0, 0, 0),
    0
));

// Using the timestamp
builder.Services.AddSnowflakeIdGenerator(new SnowflakeConfig(
    1577836800000,
    1
));

// Using the configuration class
var config = new Configuration(
    new DateTime(2024, 7, 19, 0, 0, 0, 0),
    0
);
builder.Services.AddSnowflakeIdGenerator(config);
```

## Parsing a Snowflake

Sharpflake includes methods to parse a snowflake into its components.
The parsing can be done on `string` and `long` types.

```csharp
'62937765418893312'.ToSnowflake();
```

In case you have a custom configuration, you should pass the configuration to the parser
```csharp
var config = new Configuration(
    new DateTime(2024, 7, 19, 0, 0, 0, 0),
    0
);
'62937765418893312'.ToSnowflake(config);
```

Or you can pass the generator to the parser
```csharp
var config = new Configuration(
    new DateTime(2024, 7, 19, 0, 0, 0, 0),
    0
);
var generator = new SnowflakeGenerator(config);
'62937765418893312'.ToSnowflake(generator);
```

It's also possible to type cast a `string` or `long` to a `Snowflake` object,
this will automatically parse the snowflake, but it doesn't take a custom configuration.
```csharp
var snowflake = (Snowflake)'62937765418893312';
```

## Related Projects
- [SwiftySnowflake](https://github.com/PedroCavaleiro/SwiftySnowflake) - A Swift Snowflake ID library
- [avalanche](https://github.com/PedroCavaleiro/avalanche) - A Simple TypeScript Snowflake ID library