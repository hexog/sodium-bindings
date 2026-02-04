using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Native;

public partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_secretbox_keybytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_secretbox_noncebytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_secretbox_macbytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(ConstCStringMarshaller))]
    public static partial string crypto_secretbox_primitive();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_secretbox_messagebytes_max();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_secretbox_easy(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_secretbox_open_easy(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_secretbox_detached(
        Span<byte> c,
        Span<byte> mac,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_secretbox_open_detached(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ReadOnlySpan<byte> mac,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void crypto_secretbox_keygen(Span<byte> k);

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_secretbox_zerobytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_secretbox_boxzerobytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_secretbox(
        Span<byte> c,
        ReadOnlySpan<byte> m,
        ulong mlen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_secretbox_open(
        Span<byte> m,
        ReadOnlySpan<byte> c,
        ulong clen,
        ReadOnlySpan<byte> n,
        ReadOnlySpan<byte> k
    );
}
