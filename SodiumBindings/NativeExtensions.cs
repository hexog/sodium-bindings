using System.Runtime.CompilerServices;

namespace SodiumBindings;

internal static class NativeExtensions
{
    extension(int returnValue)
    {
        public void EnsureSuccess(
            [CallerArgumentExpression(nameof(returnValue))]
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
