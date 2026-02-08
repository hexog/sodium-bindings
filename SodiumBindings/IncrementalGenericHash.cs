using System.Security.Cryptography;

namespace SodiumBindings;

public sealed class IncrementalGenericHash : IDisposable
{
    private readonly byte[] state = new byte[crypto_generichash_statebytes()];

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

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(state);
        GC.SuppressFinalize(this);
    }
}
