namespace SodiumBindings.Aead;

public static class Aegis256
{
    public static ulong AdditionalBytes => crypto_aead_aegis256_abytes();

    public static ulong KeyBytes => crypto_aead_aegis256_keybytes();

    public static ulong NonceBytes => crypto_aead_aegis256_npubbytes();

    public static ulong GetCiphertextLength(ulong plaintextLength)
    {
        return plaintextLength + AdditionalBytes;
    }

    public static int GetCiphertextLength(int plaintextLength)
    {
        return checked((int)GetCiphertextLength((ulong)plaintextLength));
    }

    public static ulong GetPlaintextLength(ulong ciphertextLength)
    {
        return ciphertextLength - AdditionalBytes;
    }

    public static int GetPlaintextLength(int ciphertextLength)
    {
        return checked((int)GetPlaintextLength((ulong)ciphertextLength));
    }

    public static void Encrypt(
        Span<byte> ciphertext,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> additionalData,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, GetCiphertextLength((ulong)plaintext.Length));
        Validate.Range(plaintext.Length, 0u, crypto_aead_aegis256_messagebytes_max());
        Validate.GreaterOrEqualTo(nonce.Length, crypto_aead_aegis256_npubbytes());
        Validate.GreaterOrEqualTo(key.Length, crypto_aead_aegis256_keybytes());

        crypto_aead_aegis256_encrypt(ciphertext, out _, plaintext, (ulong)plaintext.Length, additionalData, (ulong)additionalData.Length, null, nonce, key).EnsureSuccess();
    }

    public static bool Decrypt(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> additionalData,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, GetPlaintextLength((ulong)ciphertext.Length));
        Validate.Range(plaintext.Length, 0u, crypto_aead_aegis256_messagebytes_max());
        Validate.GreaterOrEqualTo(nonce.Length, crypto_aead_aegis256_npubbytes());
        Validate.GreaterOrEqualTo(key.Length, crypto_aead_aegis256_keybytes());

        var exitCode = crypto_aead_aegis256_decrypt(
            plaintext, out _, null, ciphertext, (ulong)ciphertext.Length, additionalData, (ulong)additionalData.Length, nonce, key
        );

        return exitCode == 0;
    }
}
