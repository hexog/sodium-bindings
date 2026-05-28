using System.Runtime.CompilerServices;

namespace SodiumBindings;

public static class KeyDerivation
{
    public static int KeyBytes { get; } = (int)crypto_kdf_keybytes();

    public static int SubkeyBytesMin { get; } = (int)crypto_kdf_bytes_min();

    public static int SubkeyBytesMax { get; } = (int)crypto_kdf_bytes_max();

    public static int HmacKeyBytes { get; } = (int)crypto_kdf_hkdf_sha256_keybytes();

    public static int HmacSubkeyBytesMin { get; } = (int)crypto_kdf_hkdf_sha256_bytes_min();

    public static int HmacSubkeyBytesMax { get; } = (int)crypto_kdf_hkdf_sha256_bytes_max();

    public static int ContextBytes { get; } = (int)crypto_kdf_contextbytes();

    public static void Derive(
        Span<byte> subkey,
        ulong subkeyId,
        ReadOnlySpan<byte> context,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(subkey.Length, SubkeyBytesMin, SubkeyBytesMax);
        Validate.GreaterOrEqualTo(context.Length, ContextBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var subkeyLen = (nuint)subkey.Length;
        crypto_kdf_derive_from_key(subkey, subkeyLen, subkeyId, context, key).EnsureSuccess();
    }

    public static void HmacDerive(
        Span<byte> subkey,
        ReadOnlySpan<byte> context,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(subkey.Length, HmacSubkeyBytesMin, HmacSubkeyBytesMax);
        Validate.GreaterOrEqualTo(key.Length, HmacKeyBytes);

        var subkeyLen = (nuint)subkey.Length;
        var contextLen = (nuint)context.Length;
        crypto_kdf_hkdf_sha256_expand(subkey, subkeyLen, context, contextLen, key).EnsureSuccess();
    }

    public static void Extract(
        Span<byte> key,
        ReadOnlySpan<byte> salt,
        ReadOnlySpan<byte> inputKeyingMaterial
    )
    {
        Validate.GreaterOrEqualTo(key.Length, HmacKeyBytes);

        var saltLen = (nuint)salt.Length;
        var ikmLen = (nuint)inputKeyingMaterial.Length;
        crypto_kdf_hkdf_sha256_extract(key, salt, saltLen, inputKeyingMaterial, ikmLen).EnsureSuccess();
    }

    public static IncrementalKeyExtractor CreateExtractor(ReadOnlySpan<byte> salt)
    {
        var extractor = new IncrementalKeyExtractor();
        extractor.Initialize(salt);
        return extractor;
    }
}