using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Native;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_seedbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_publickeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_secretkeybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_noncebytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_macbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_messagebytes_max();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ConstCStringMarshaller))]
    public static partial string crypto_box_primitive();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_seed_keypair(
        Span<byte> pk,
        Span<byte> sk,
        ReadOnlySpan<byte> seed
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_keypair(
        Span<byte> pk,
        Span<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_easy(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_open_easy(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_detached(
        Span<byte> c,
        Span<byte> mac,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_open_detached(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ReadOnlySpan<byte> mac,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_beforenmbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_beforenm(
        Span<byte> k,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_easy_afternm(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_open_easy_afternm(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_detached_afternm(
        Span<byte> c,
        Span<byte> mac,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_open_detached_afternm(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ReadOnlySpan<byte> mac,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_sealbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_seal(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> pk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_seal_open(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_box_zerobytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_open(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> pk,
        ReadOnlySpan<byte> sk
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_afternm(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_box_open_afternm(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );
}
