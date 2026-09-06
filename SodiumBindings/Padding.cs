namespace SodiumBindings;

public static class Padding
{
    public static int Pad(
        Span<byte> buffer,
        int unpaddedLength,
        int blockSize
    )
    {
        Validate.Range(unpaddedLength, 0, buffer.Length);
        Validate.GreaterOrEqualTo(blockSize, 1);

        var bufferLength = (nuint)buffer.Length;
        sodium_pad(out var paddedLength, buffer, (nuint)unpaddedLength, (nuint)blockSize, bufferLength).EnsureSuccess();

        return (int)paddedLength;
    }

    public static bool Unpad(
        ReadOnlySpan<byte> buffer,
        int blockSize,
        out int unpaddedLength
    )
    {
        Validate.GreaterOrEqualTo(blockSize, 1);

        var bufferLength = (nuint)buffer.Length;
        var exitCode = sodium_unpad(
            out var nativeUnpaddedLength,
            buffer,
            bufferLength,
            (nuint)blockSize
        );

        if (exitCode == 0)
        {
            unpaddedLength = (int)nativeUnpaddedLength;
            return true;
        }

        unpaddedLength = 0;
        return false;
    }
}
