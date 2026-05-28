using System.Security.Cryptography;

namespace SodiumBindings;

public sealed class IncrementalGenericHash : IDisposable
{
    private readonly byte[] state = new byte[crypto_generichash_statebytes()];

    internal IncrementalGenericHash()
    {
    }

    public void Initialize(int outputLength, ReadOnlySpan<byte> key = default)
    {
        var keylen = (nuint)key.Length;
        var outlen = (nuint)outputLength;
        crypto_generichash_init(state, key, keylen, outlen).EnsureSuccess();
    }

    public void Update(ReadOnlySpan<byte> input)
    {
        var inlen = (nuint)input.Length;
        crypto_generichash_update(state, input, inlen).EnsureSuccess();
    }

    public void Final(Span<byte> output)
    {
        var outlen = (nuint)output.Length;
        crypto_generichash_final(state, output, outlen).EnsureSuccess();
    }

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(state);
    }
}
