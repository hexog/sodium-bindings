using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SodiumBindings.Native;

[SuppressMessage("ReSharper", "InconsistentNaming")]
internal static partial class Sodium
{
    private const string LibraryName = "libsodium";

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int sodium_init();
}
