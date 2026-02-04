using System.Runtime.CompilerServices;

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

    public struct IncrementalGenericHash
    {
        private State state;

        public void Initialize(ReadOnlySpan<byte> key, int outputLength)
        {
            crypto_generichash_init(state, key, (nuint)key.Length, (nuint)outputLength).EnsureSuccess();
        }

        public void Update(ReadOnlySpan<byte> input)
        {
            crypto_generichash_update(state, input, (nuint)input.Length).EnsureSuccess();
        }

        public void Final(Span<byte> output)
        {
            crypto_generichash_final(state, output, (nuint)output.Length);
        }

        [InlineArray((int)StateBytes)]
        private struct State
        {
            public byte Element0;
        }
    }
}
