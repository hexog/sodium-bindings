namespace SodiumBindings;

public static class RandomBytes
{
    public static void NextBytes(Span<byte> buffer)
    {
        randombytes_buf(buffer, (nuint)buffer.Length);
    }

    public static uint Next()
    {
        return randombytes_random();
    }
}
