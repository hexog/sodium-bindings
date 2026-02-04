using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.Marshalling;

namespace SodiumBindings.Native;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(ConstCStringMarshaller))]
internal static unsafe class ConstCStringMarshaller
{
    public static uint* ConvertToUnmanaged(string? managed)
        => throw new NotSupportedException();

    public static string? ConvertToManaged(uint* unmanaged)
    {
        return AnsiStringMarshaller.ConvertToManaged((byte*)((nuint)unmanaged).ToPointer());
    }

    public static void Free([SuppressMessage("ReSharper", "UnusedParameter.Local")] uint* unmanaged)
    {
        // don't free constant
    }
}
