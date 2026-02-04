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
        Validate.Equals(context.Length, crypto_kdf_contextbytes());
        Validate.Equals(key.Length, crypto_kdf_keybytes());

        crypto_kdf_derive_from_key(subkey, (nuint)subkey.Length, subkeyId, context, key).EnsureSuccess();
    }

    public static void HmacDerive(
        Span<byte> subkey,
        ReadOnlySpan<byte> context,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(subkey.Length, crypto_kdf_hkdf_sha256_bytes_min(), crypto_kdf_hkdf_sha256_bytes_max());
        Validate.Equals(key.Length, crypto_kdf_hkdf_sha256_keybytes());

        crypto_kdf_hkdf_sha256_expand(subkey, (nuint)subkey.Length, context, (nuint)subkey.Length, key).EnsureSuccess();
    }

    public static void Extract(
        Span<byte> key,
        ReadOnlySpan<byte> salt,
        ReadOnlySpan<byte> inputKeyingMaterial
    )
    {
        Validate.Equals(key.Length, crypto_kdf_hkdf_sha256_keybytes());

        crypto_kdf_hkdf_sha256_extract(key, salt, (nuint)salt.Length, inputKeyingMaterial, (nuint)inputKeyingMaterial.Length).EnsureSuccess();
    }

    public static Extractor CreateExtractor(ReadOnlySpan<byte> salt)
    {
        var extractor = new Extractor();
        extractor.Initialize(salt);
        return extractor;
    }

    public struct Extractor
    {
        private State state;

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
            Validate.Equals(key.Length, crypto_kdf_hkdf_sha256_keybytes());

            crypto_kdf_hkdf_sha256_extract_final(state, key).EnsureSuccess();
        }

        [InlineArray((int)StateBytes)]
        private struct State
        {
            public byte Element0;
        }
    }
}
