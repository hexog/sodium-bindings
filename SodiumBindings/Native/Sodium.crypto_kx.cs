using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Native;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kx_publickeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kx_secretkeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kx_seedbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_kx_sessionkeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ConstCStringMarshaller))]
    public static partial string crypto_kx_primitive();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kx_seed_keypair(
        Span<byte> pk,
        Span<byte> sk,
        ReadOnlySpan<byte> seed
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kx_keypair(
        Span<byte> pk,
        Span<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kx_client_session_keys(
        Span<byte> rx,
        Span<byte> tx,
        ReadOnlySpan<byte> client_pk,
        ReadOnlySpan<byte> client_sk,
        ReadOnlySpan<byte> server_pk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_kx_server_session_keys(
        Span<byte> rx,
        Span<byte> tx,
        ReadOnlySpan<byte> server_pk,
        ReadOnlySpan<byte> server_sk,
        ReadOnlySpan<byte> client_pk
    );
}
