using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SodiumBindings.Native;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint randombytes_seedbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void randombytes_buf(
        Span<byte> buf,
        nuint size
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void randombytes_buf_deterministic(
        Span<byte> buf,
        nuint size,
        ReadOnlySpan<byte> seed
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint randombytes_random();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint randombytes_uniform(uint upper_bound);
}
