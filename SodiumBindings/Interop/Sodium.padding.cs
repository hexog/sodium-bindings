using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SodiumBindings.Interop;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int sodium_pad(
        out nuint padded_buflen_p,
        Span<byte> buf,
        nuint unpadded_buflen,
        nuint blocksize,
        nuint max_buflen
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int sodium_unpad(
        out nuint unpadded_buflen_p,
        ReadOnlySpan<byte> buf,
        nuint padded_buflen,
        nuint blocksize
    );
}
