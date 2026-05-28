using System.Numerics;
using System.Runtime.CompilerServices;

namespace SodiumBindings;

internal static class Validate
{
    public static void Range<TNumber>(
        TNumber value,
        TNumber min,
        TNumber max,
        [CallerArgumentExpression(nameof(value))]
        string? valueExpression = null,
        [CallerArgumentExpression(nameof(min))]
        string? minExpression = null,
        [CallerArgumentExpression(nameof(max))]
        string? maxExpression = null
    ) where TNumber : INumber<TNumber>
    {
        if (value < min || value > max)
        {
            throw new SodiumException(
                $"Expected '{valueExpression}' to be between '{minExpression}' = {min} and '{maxExpression}' = {max} but found {value}");
        }
    }

    public static void GreaterOrEqualTo<TNumber>(
        TNumber value, TNumber expected,
        [CallerArgumentExpression(nameof(value))]
        string? valueExpression = null,
        [CallerArgumentExpression(nameof(expected))]
        string? expectedExpression = null
    ) where TNumber : INumber<TNumber>
    {
        if (value < expected)
        {
            throw new SodiumException($"Expected '{valueExpression}' = {value} to be '{expectedExpression}' = {expected} or greater");
        }
    }
}
