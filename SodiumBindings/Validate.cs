using System.Numerics;
using System.Runtime.CompilerServices;

namespace SodiumBindings;

internal static class Validate
{
    public static void Range<TActualNumber, TExpectedNumber>(
        TActualNumber value,
        TExpectedNumber min,
        TExpectedNumber max,
        [CallerArgumentExpression(nameof(value))]
        string? valueExpression = null,
        [CallerArgumentExpression(nameof(min))]
        string? minExpression = null,
        [CallerArgumentExpression(nameof(max))]
        string? maxExpression = null
    )
        where TActualNumber : INumber<TActualNumber>
        where TExpectedNumber : INumber<TExpectedNumber>
    {
        var valueNumber = TExpectedNumber.CreateChecked(value);
        if (valueNumber < min || valueNumber > max)
        {
            throw new SodiumException(
                $"Expected '{valueExpression}' to be between '{minExpression}' = {min} and '{maxExpression}' = {max} but found {value}");
        }
    }

    public static void Equals<TActualNumber, TExpectedNumber>(
        TActualNumber value, TExpectedNumber expected,
        [CallerArgumentExpression(nameof(value))]
        string? valueExpression = null,
        [CallerArgumentExpression(nameof(expected))]
        string? expectedExpression = null
    )
        where TActualNumber : INumber<TActualNumber>
        where TExpectedNumber : INumber<TExpectedNumber>
    {
        var valueNumber = TExpectedNumber.CreateChecked(value);
        if (valueNumber != expected)
        {
            throw new SodiumException($"Expected '{valueExpression}' = {value} to be '{expectedExpression}' = {expected}");
        }
    }

    public static void GreaterOrEqualTo<TActualNumber, TExpectedNumber>(
        TActualNumber value, TExpectedNumber expected,
        [CallerArgumentExpression(nameof(value))]
        string? valueExpression = null,
        [CallerArgumentExpression(nameof(expected))]
        string? expectedExpression = null
    )
        where TActualNumber : INumber<TActualNumber>
        where TExpectedNumber : INumber<TExpectedNumber>
    {
        var valueNumber = TExpectedNumber.CreateChecked(value);
        if (valueNumber < expected)
        {
            throw new SodiumException($"Expected '{valueExpression}' = {value} to be '{expectedExpression}' = {expected} or greater");
        }
    }
}
