namespace SodiumBindings;

public static class Box
{
    public static int PublicKeyBytes { get; } = (int)crypto_box_publickeybytes();

    public static int SecretKeyBytes { get; } = (int)crypto_box_secretkeybytes();

    public static int MacBytes { get; } = (int)crypto_box_macbytes();

    public static int NonceBytes { get; } = (int)crypto_box_noncebytes();

    public static int SeedBytes { get; } = (int)crypto_box_seedbytes();

    public static int BeforeNonceMessageBytes { get; } = (int)crypto_box_beforenmbytes();

    public static int PrecalculatedKeyBytes => BeforeNonceMessageBytes;

    public static int GetCiphertextLength(int plaintextLength)
    {
        return MacBytes + plaintextLength;
    }

    public static int GetPlaintextLength(int ciphertextLength)
    {
        return ciphertextLength - MacBytes;
    }

    public static void GenerateKeyPair(
        Span<byte> publicKey,
        Span<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        crypto_box_keypair(publicKey, secretKey).EnsureSuccess();
    }

    public static void GenerateKeyPairFromSeed(
        Span<byte> publicKey,
        Span<byte> secretKey,
        ReadOnlySpan<byte> seed
    )
    {
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);
        Validate.GreaterOrEqualTo(seed.Length, SeedBytes);

        crypto_box_seed_keypair(publicKey, secretKey, seed).EnsureSuccess();
    }

    public static void Encrypt(
        Span<byte> ciphertext,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> publicKey,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, GetCiphertextLength(plaintext.Length));
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        var plen = (ulong)plaintext.Length;
        crypto_box_easy(ciphertext, plaintext, plen, nonce, publicKey, secretKey).EnsureSuccess();
    }

    public static void Decrypt(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> publicKey,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, GetPlaintextLength(ciphertext.Length));
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        var clen = (ulong)ciphertext.Length;
        crypto_box_open_easy(plaintext, ciphertext, clen, nonce, publicKey, secretKey).EnsureSuccess();
    }

    public static void EncryptDetached(
        Span<byte> ciphertext,
        Span<byte> mac,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> publicKey,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, plaintext.Length);
        Validate.GreaterOrEqualTo(mac.Length, MacBytes);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        var plen = (ulong)plaintext.Length;
        crypto_box_detached(ciphertext, mac, plaintext, plen, nonce, publicKey, secretKey).EnsureSuccess();
    }

    public static void DecryptDetached(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> mac,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> publicKey,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, ciphertext.Length);
        Validate.GreaterOrEqualTo(mac.Length, MacBytes);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        var clen = (ulong)ciphertext.Length;
        crypto_box_open_detached(plaintext, ciphertext, mac, clen, nonce, publicKey, secretKey).EnsureSuccess();
    }

    public static void GeneratePrecalculatedKey(
        Span<byte> key,
        ReadOnlySpan<byte> publicKey,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(key.Length, PrecalculatedKeyBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        crypto_box_beforenm(key, publicKey, secretKey).EnsureSuccess();
    }

    public static void EncryptPrecalculated(
        Span<byte> ciphertext,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, GetCiphertextLength(plaintext.Length));
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, PrecalculatedKeyBytes);

        var plen = (ulong)plaintext.Length;
        crypto_box_easy_afternm(ciphertext, plaintext, plen, nonce, key).EnsureSuccess();
    }

    public static void DecryptPrecalculated(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, GetPlaintextLength(ciphertext.Length));
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, PrecalculatedKeyBytes);

        var clen = (ulong)ciphertext.Length;
        crypto_box_open_easy_afternm(plaintext, ciphertext, clen, nonce, key).EnsureSuccess();
    }

    public static void EncryptPrecalculatedDetached(
        Span<byte> ciphertext,
        Span<byte> mac,
        ReadOnlySpan<byte> plaintext,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, plaintext.Length);
        Validate.GreaterOrEqualTo(mac.Length, MacBytes);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, PrecalculatedKeyBytes);

        var plen = (ulong)plaintext.Length;
        crypto_box_detached_afternm(ciphertext, mac, plaintext, plen, nonce, key).EnsureSuccess();
    }

    public static void DecryptPrecalculatedDetached(
        Span<byte> plaintext,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> mac,
        ReadOnlySpan<byte> nonce,
        ReadOnlySpan<byte> key
    )
    {
        Validate.GreaterOrEqualTo(plaintext.Length, ciphertext.Length);
        Validate.GreaterOrEqualTo(mac.Length, MacBytes);
        Validate.GreaterOrEqualTo(nonce.Length, NonceBytes);
        Validate.GreaterOrEqualTo(key.Length, PrecalculatedKeyBytes);

        var clen = (ulong)ciphertext.Length;
        crypto_box_open_detached_afternm(plaintext, ciphertext, mac, clen, nonce, key).EnsureSuccess();
    }
}
