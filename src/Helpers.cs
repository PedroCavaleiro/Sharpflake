using System.Globalization;
using System.Runtime.CompilerServices;

namespace Sharpflake;

public static class Helpers {

    /// <summary>
    /// Waits until the next timestamp is generated to prevent collision
    /// </summary>
    /// <param name="currentTimestamp">The current timestamp</param>
    /// <returns>The new timestamp</returns>
    internal static long WaitUntilNextTimestamp(long currentTimestamp) {
        var nextTimestamp = GenerateTimestamp(DateTime.UtcNow);
        while (nextTimestamp <= currentTimestamp)
            nextTimestamp = GenerateTimestamp(DateTime.UtcNow);

        return nextTimestamp;
    }

    /// <summary>
    /// Generates a timestamp in milliseconds
    /// </summary>
    /// <param name="dateTime">DateTime to generate the epoch from</param>
    /// <returns>The epoch in milliseconds</returns>
    internal static long GenerateTimestamp(DateTime dateTime) {
        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return Convert.ToInt64(dateTime.Subtract(unixEpoch).TotalMilliseconds.ToString(CultureInfo.InvariantCulture).Split(".")[0]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static long GetMask(byte bits) => (1L << bits) - 1;

}
