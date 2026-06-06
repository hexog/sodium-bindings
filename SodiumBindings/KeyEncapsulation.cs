namespace SodiumBindings;

public static class KeyEncapsulation
{
    public static int PublicKeyBytes { get; } = (int)crypto_kem_publickeybytes();

    public static int SecretKeyBytes { get; } = (int)crypto_kem_secretkeybytes();

    public static int CiphertextBytes { get; } = (int)crypto_kem_ciphertextbytes();

    public static int SharedSecretBytes { get; } = (int)crypto_kem_sharedsecretbytes();

    public static int SeedBytes { get; } = (int)crypto_kem_seedbytes();

    public static void GenerateKeyPair(
        Span<byte> publicKey,
        Span<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        crypto_kem_keypair(publicKey, secretKey).EnsureSuccess();
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

        crypto_kem_seed_keypair(publicKey, secretKey, seed).EnsureSuccess();
    }

    public static void Encrypt(
        Span<byte> ciphertext,
        Span<byte> sharedSecret,
        ReadOnlySpan<byte> publicKey
    )
    {
        Validate.GreaterOrEqualTo(ciphertext.Length, CiphertextBytes);
        Validate.GreaterOrEqualTo(sharedSecret.Length, SharedSecretBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);

        crypto_kem_enc(ciphertext, sharedSecret, publicKey).EnsureSuccess();
    }

    public static void Decrypt(
        Span<byte> sharedSecret,
        ReadOnlySpan<byte> ciphertext,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(sharedSecret.Length, SharedSecretBytes);

        crypto_kem_dec(sharedSecret, ciphertext, secretKey).EnsureSuccess();
    }
}
