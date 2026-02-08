namespace SodiumBindings;

public static class GenericHash
{
    internal const uint StateBytes = 384;

    public static void Hash(
        Span<byte> output,
        ReadOnlySpan<byte> input,
        ReadOnlySpan<byte> key = default
    )
    {
        Validate.Range(output.Length, crypto_generichash_bytes_min(), crypto_generichash_bytes_max());
        if (!key.IsEmpty)
        {
            Validate.Range(key.Length, crypto_generichash_keybytes_min(), crypto_generichash_keybytes_max());
        }

        crypto_generichash(output, (nuint)output.Length, input, (ulong)input.Length, key, (nuint)key.Length).EnsureSuccess();
    }

    public static IncrementalGenericHash Create(
        int outputLength,
        ReadOnlySpan<byte> key = default
    )
    {
        var hash = new IncrementalGenericHash();
        hash.Initialize(key, outputLength);
        return hash;
    }
}