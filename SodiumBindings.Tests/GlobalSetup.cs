global using static SodiumBindings.Native.Sodium;

[assembly: Retry(3)]
[assembly: System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]

namespace SodiumBindings.Tests;

public class GlobalHooks
{
    [Before(TestSession)]
    public static async Task SetUp()
    {
        await sodium_init().EnsureSuccess();
    }
}
