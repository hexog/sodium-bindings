namespace SodiumBindings;

public static class GenericHash
{
    public static int BytesMin { get; } = (int)crypto_generichash_bytes_min();

    public static int BytesMax { get; } = (int)crypto_generichash_bytes_max();

    public static int KeyBytesMin { get; } = (int)crypto_generichash_keybytes_min();

    public static int KeyBytesMax { get; } = (int)crypto_generichash_keybytes_max();

    public static void Hash(
        Span<byte> output,
        ReadOnlySpan<byte> input,
        ReadOnlySpan<byte> key = default
    )
    {
        Validate.Range(output.Length, BytesMin, BytesMax);
        if (!key.IsEmpty)
        {
            Validate.Range(key.Length, KeyBytesMin, KeyBytesMax);
        }

        var outputLength = (nuint)output.Length;
        var inputLength = (ulong)input.Length;
        var keyLength = (nuint)key.Length;
        crypto_generichash(output, outputLength, input, inputLength, key, keyLength).EnsureSuccess();
    }

    public static IncrementalGenericHash Create(
        int outputLength,
        ReadOnlySpan<byte> key = default
    )
    {
        var hash = new IncrementalGenericHash();
        hash.Initialize(outputLength, key);
        return hash;
    }
}