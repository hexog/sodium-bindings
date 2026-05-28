namespace SodiumBindings;

public static class SecretBox
{
    public static int NonceBytes { get; } = (int)crypto_secretbox_noncebytes();

    public static int KeyBytes { get; } = (int)crypto_secretbox_keybytes();

    public static int MacBytes { get; } = (int)crypto_secretbox_macbytes();

    public static int GetCiphertextLength(int plaintextLength)
    {
        return MacBytes + plaintextLength;
    }

    public static int GetPlaintextLength(int ciphertextLength)
    {
        return ciphertextLength - MacBytes;
    }

    public static void Encrypt(
        Span<byte> ciphertext,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, GetCiphertextLength(plaintext.Length));
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var plaintextLength = (ulong)plaintext.Length;
        crypto_secretbox_easy(ciphertext, plaintext, plaintextLength, nonce, key).EnsureSuccess();
    }

    public static bool Decrypt(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, GetPlaintextLength(ciphertext.Length));
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, KeyBytes);

        var ciphertextLength = (ulong)ciphertext.Length;
        var exitCode = crypto_secretbox_open_easy(plaintext, ciphertext, ciphertextLength, nonce, key);
        return exitCode == 0;
    }
}
