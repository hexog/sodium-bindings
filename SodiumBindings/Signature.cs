namespace SodiumBindings;

public static class Signature
{
    public static ulong PublicKeyBytes => crypto_sign_publickeybytes();

    public static ulong SecretKeyBytes => crypto_sign_secretkeybytes();

    public static ulong SignatureBytes => crypto_sign_bytes();

    public static ulong SeedBytes => crypto_sign_seedbytes();

    public static void GenerateKeyPair(
        Span<byte> publicKey,
        Span<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        crypto_sign_keypair(publicKey, secretKey).EnsureSuccess();
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

        crypto_sign_seed_keypair(publicKey, secretKey, seed).EnsureSuccess();
    }

    public static void SignDetached(
        Span<byte> signature,
        ReadOnlySpan<byte> message,
        ReadOnlySpan<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(secretKey.Length, crypto_sign_secretkeybytes());

        crypto_sign_detached(signature, out _, message, (ulong)message.Length, secretKey).EnsureSuccess();
    }

    public static bool VerifyDetached(
        ReadOnlySpan<byte> signature,
        ReadOnlySpan<byte> message,
        ReadOnlySpan<byte> publicKey
    )
    {
        Validate.GreaterOrEqualTo(signature.Length, crypto_sign_bytes());
        Validate.GreaterOrEqualTo(publicKey.Length, crypto_sign_publickeybytes());

        var exitCode = crypto_sign_verify_detached(
            signature,
            message, (ulong)message.Length,
            publicKey
        );

        return exitCode == 0;
    }

    public static IncrementalSignature Create()
    {
        var incrementalSignature = new IncrementalSignature();
        incrementalSignature.Initialize();
        return incrementalSignature;
    }
}
