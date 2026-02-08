using System.Runtime.CompilerServices;

namespace SodiumBindings;

public static class KeyDerivation
{
    internal const uint StateBytes = 208;

    public static ulong KeyBytes => crypto_kdf_keybytes();

    public static ulong HmacKeyBytes => crypto_kdf_hkdf_sha256_keybytes();

    public static void Derive(
        Span<byte> subkey,
        ulong subkeyId,
        ReadOnlySpan<byte> context,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(subkey.Length, crypto_kdf_bytes_min(), crypto_kdf_bytes_max());
        Validate.GreaterOrEqualTo(context.Length, crypto_kdf_contextbytes());
        Validate.GreaterOrEqualTo(key.Length, crypto_kdf_keybytes());

        crypto_kdf_derive_from_key(subkey, (nuint)subkey.Length, subkeyId, context, key).EnsureSuccess();
    }

    public static void HmacDerive(
        Span<byte> subkey,
        ReadOnlySpan<byte> context,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(subkey.Length, crypto_kdf_hkdf_sha256_bytes_min(), crypto_kdf_hkdf_sha256_bytes_max());
        Validate.GreaterOrEqualTo(key.Length, crypto_kdf_hkdf_sha256_keybytes());

        crypto_kdf_hkdf_sha256_expand(subkey, (nuint)subkey.Length, context, (nuint)subkey.Length, key).EnsureSuccess();
    }

    public static void Extract(
        Span<byte> key,
        ReadOnlySpan<byte> salt,
        ReadOnlySpan<byte> inputKeyingMaterial
    )
    {
        Validate.GreaterOrEqualTo(key.Length, crypto_kdf_hkdf_sha256_keybytes());

        crypto_kdf_hkdf_sha256_extract(key, salt, (nuint)salt.Length, inputKeyingMaterial, (nuint)inputKeyingMaterial.Length).EnsureSuccess();
    }

    public static PseudorandomKeyExtractor CreateExtractor(ReadOnlySpan<byte> salt)
    {
        var extractor = new PseudorandomKeyExtractor();
        extractor.Initialize(salt);
        return extractor;
    }
}