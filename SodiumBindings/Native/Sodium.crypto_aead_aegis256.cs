using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SodiumBindings.Native;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_aead_aegis256_keybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_aead_aegis256_nsecbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_aead_aegis256_npubbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_aead_aegis256_abytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_aead_aegis256_messagebytes_max();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_aead_aegis256_encrypt(
        Span<byte> c,
        out ulong clen_p,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> ad,
        ulong adlen,
        ReadOnlySpan<byte> nsec,
        ReadOnlySpan<byte> npub,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_aead_aegis256_decrypt(
        Span<byte> m,
        out ulong mlen_p,
        Span<byte> nsec,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> ad,
        ulong adlen,
        ReadOnlySpan<byte> npub,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_aead_aegis256_encrypt_detached(
        Span<byte> c,
        Span<byte> mac,
        out ulong maclen_p,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> ad,
        ulong adlen,
        ReadOnlySpan<byte> nsec,
        ReadOnlySpan<byte> npub,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_aead_aegis256_decrypt_detached(
        Span<byte> m,
        Span<byte> nsec,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> mac,
        ReadOnlySpan<byte> ad,
        ulong adlen,
        ReadOnlySpan<byte> npub,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void crypto_aead_aegis256_keygen(Span<byte> k);
}
