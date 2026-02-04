using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Native;

public partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kdf_hkdf_sha256_keybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kdf_hkdf_sha256_bytes_min();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kdf_hkdf_sha256_bytes_max();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kdf_hkdf_sha256_extract(
        Span<byte> prk,
        ReadOnlySpan<byte> salt,
        nuint salt_len,
        ReadOnlySpan<byte> ikm,
        nuint ikm_len
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void crypto_kdf_hkdf_sha256_keygen(Span<byte> prk);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kdf_hkdf_sha256_expand(
        Span<byte> @out,
        nuint out_len,
        ReadOnlySpan<byte> ctx,
        nuint ctx_len,
        ReadOnlySpan<byte> prk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kdf_hkdf_sha256_statebytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kdf_hkdf_sha256_extract_init(
        Span<byte> state,
        ReadOnlySpan<byte> salt,
        nuint salt_len
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kdf_hkdf_sha256_extract_update(
        Span<byte> state,
        ReadOnlySpan<byte> ikm,
        nuint ikm_len
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kdf_hkdf_sha256_extract_final(
        Span<byte> state,
        Span<byte> prk
    );
}
