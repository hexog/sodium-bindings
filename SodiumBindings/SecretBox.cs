namespace SodiumBindings;

public static class SecretBox
{
    public static ulong NonceBytes => crypto_secretbox_noncebytes();

    public static ulong KeyBytes => crypto_secretbox_keybytes();

    public static ulong MacBytes => crypto_secretbox_macbytes();

    public static void Encrypt(
        Span<byte> ciphertext,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(plaintext.Length, 0u, crypto_secretbox_messagebytes_max());
        Validate.Equals(ciphertext.Length, MacBytes + (ulong)plaintext.Length);
        Validate.Equals(nonce.Length, crypto_secretbox_noncebytes());
        Validate.Equals(key.Length, crypto_secretbox_keybytes());

        crypto_secretbox_easy(ciphertext, plaintext, (ulong)plaintext.Length, nonce, key).EnsureSuccess();
    }

    public static void Decrypt(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.Range(ciphertext.Length, 0u, crypto_secretbox_messagebytes_max() + MacBytes);
        Validate.Equals(plaintext.Length, (ulong)ciphertext.Length - MacBytes);
        Validate.Equals(nonce.Length, crypto_secretbox_noncebytes());
        Validate.Equals(key.Length, crypto_secretbox_keybytes());

        crypto_secretbox_open_easy(plaintext, ciphertext, (ulong)ciphertext.Length, nonce, key).EnsureSuccess();
    }
}
