using System.Security.Cryptography;

namespace SodiumBindings;

public sealed class PseudorandomKeyExtractor : IDisposable
{
    private readonly byte[] state = new byte[crypto_kdf_hkdf_sha256_statebytes()];

    public void Initialize(ReadOnlySpan<byte> salt)
    {
        crypto_kdf_hkdf_sha256_extract_init(state, salt, (nuint)salt.Length).EnsureSuccess();
    }

    public void Update(ReadOnlySpan<byte> inputKeyingMaterial)
    {
        crypto_kdf_hkdf_sha256_extract_update(state, inputKeyingMaterial, (nuint)inputKeyingMaterial.Length).EnsureSuccess();
    }

    public void Final(Span<byte> key)
    {
        Validate.GreaterOrEqualTo(key.Length, crypto_kdf_hkdf_sha256_keybytes());

        crypto_kdf_hkdf_sha256_extract_final(state, key).EnsureSuccess();
    }

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(state);
    }
}
