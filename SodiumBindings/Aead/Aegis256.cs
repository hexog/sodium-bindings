namespace SodiumBindings.Aead;

public static class Aegis256
{
    public static int AdditionalBytes { get; } = (int)crypto_aead_aegis256_abytes();

    public static int KeyBytes { get; } = (int)crypto_aead_aegis256_keybytes();

    public static int NonceBytes { get; } = (int)crypto_aead_aegis256_npubbytes();

    public static long MessageBytesMax { get; } = (long)crypto_aead_aegis256_messagebytes_max();

    public static int GetCiphertextLength(int plaintextLength)
    {
        return plaintextLength + AdditionalBytes;
    }

    public static int GetPlaintextLength(int ciphertextLength)
    {
        return ciphertextLength - AdditionalBytes;
    }

    public static byte[] GenerateKey()
    {
        var key = new byte[KeyBytes];
        GenerateKey(key);
        return key;
    }

    public static void GenerateKey(Span<byte> key)
    {
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        crypto_aead_aegis256_keygen(key);
    }

    public static void Encrypt(
        Span<byte> ciphertext,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> additionalData,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key,
        out int ciphertextLength
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, GetCiphertextLength(plaintext.Length));
        Validate.Range(plaintext.Length, 0, MessageBytesMax);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var plen = (ulong)plaintext.Length;
        var adlen = (ulong)additionalData.Length;
        crypto_aead_aegis256_encrypt(ciphertext, out var clen, plaintext, plen, additionalData, adlen, null, nonce, key).EnsureSuccess();
        ciphertextLength = (int)clen;
    }

    public static bool Decrypt(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> additionalData,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key,
        out int plaintextLength
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, GetPlaintextLength(ciphertext.Length));
        Validate.Range(plaintext.Length, 0, MessageBytesMax);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var clen = (ulong)ciphertext.Length;
        var adlen = (ulong)additionalData.Length;
        var exitCode = crypto_aead_aegis256_decrypt(plaintext, out var mlen, null, ciphertext, clen, additionalData, adlen, nonce, key);
        plaintextLength = (int)mlen;
        return exitCode == 0;
    }

    public static void EncryptDetached(
        Span<byte> ciphertext,
        Span<byte> mac,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> additionalData,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key,
        out int macLength
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, plaintext.Length);
        Validate.Range(plaintext.Length, 0, MessageBytesMax);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var plen = (ulong)plaintext.Length;
        var adlen = (ulong)additionalData.Length;
        crypto_aead_aegis256_encrypt_detached(ciphertext, mac, out var maclen, plaintext, plen, additionalData, adlen, null, nonce, key).EnsureSuccess();
        macLength = (int)maclen;
    }

    public static bool DecryptDetached(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> mac,
        ReadOnlySpan<byte> additionalData,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, ciphertext.Length);
        Validate.Range(plaintext.Length, 0, MessageBytesMax);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var clen = (ulong)ciphertext.Length;
        var adlen = (ulong)additionalData.Length;
        var exitCode = crypto_aead_aegis256_decrypt_detached(plaintext, null, ciphertext, clen, mac, additionalData, adlen, nonce, key);
        return exitCode == 0;
    }
}
