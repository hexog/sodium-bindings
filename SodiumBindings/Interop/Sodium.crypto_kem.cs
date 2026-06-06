using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Interop;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kem_publickeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kem_secretkeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kem_ciphertextbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kem_sharedsecretbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kem_seedbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ConstCStringMarshaller))]
    public static partial string crypto_kem_primitive();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kem_seed_keypair(
        Span<byte> pk,
        Span<byte> sk,
        ReadOnlySpan<byte> seed
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kem_keypair(
        Span<byte> pk,
        Span<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kem_enc(
        Span<byte> ct,
        Span<byte> ss,
        ReadOnlySpan<byte> pk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kem_dec(
        Span<byte> ss,
        ReadOnlySpan<byte> ct,
        ReadOnlySpan<byte> sk
    );
}
