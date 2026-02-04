using System.Runtime.CompilerServices;

namespace SodiumBindings;

internal static class NativeExtensions
{
    extension(int returnValue)
    {
        public void EnsureSuccess(
#pragma warning disable CS8963 // The CallerArgumentExpressionAttribute is applied with an invalid parameter name.
            [CallerArgumentExpression(nameof(returnValue))]
#pragma warning restore CS8963 // The CallerArgumentExpressionAttribute is applied with an invalid parameter name.
            string? expression = null
        )
        {
            if (returnValue != 0)
            {
                throw new SodiumException($"Unsuccessful exit code from '{expression}': {returnValue}");
            }
        }
    }
}
