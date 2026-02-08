using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Native;

internal static partial class Sodium
{
    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_auth_bytes();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint crypto_auth_keybytes();

    [LibraryImport(LibraryName)]
    [return: MarshalUsing(typeof(ConstCStringMarshaller))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string crypto_auth_primitive();

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_auth(
        Span<byte> @out,
        ReadOnlySpan<byte> @in,
        ulong inlen,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int crypto_auth_verify(
        ReadOnlySpan<byte> h,
        ReadOnlySpan<byte> @in,
        ulong inlen,
        ReadOnlySpan<byte> k
    );

    [LibraryImport(LibraryName)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void crypto_auth_keygen(Span<byte> k);
}
