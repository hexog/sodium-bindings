using System.Security.Cryptography;

namespace SodiumBindings;

public sealed class IncrementalKeyExtractor : IDisposable
{
    private readonly byte[] state = new byte[crypto_kdf_hkdf_sha256_statebytes()];

    internal IncrementalKeyExtractor()
    {
    }

    public void Initialize(ReadOnlySpan<byte> salt)
    {
        var saltLen = (nuint)salt.Length;
        crypto_kdf_hkdf_sha256_extract_init(state, salt, saltLen).EnsureSuccess();
    }

    public void Update(ReadOnlySpan<byte> inputKeyingMaterial)
    {
        var ikmLen = (nuint)inputKeyingMaterial.Length;
        crypto_kdf_hkdf_sha256_extract_update(state, inputKeyingMaterial, ikmLen).EnsureSuccess();
    }

    public void Final(Span<byte> key)
    {
        Validate.GreaterOrEqualTo(key.Length, KeyDerivation.HmacKeyBytes);

        crypto_kdf_hkdf_sha256_extract_final(state, key).EnsureSuccess();
    }

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(state);
    }
}
