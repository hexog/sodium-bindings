namespace SodiumBindings;

public static class Signature
{
    public static int PublicKeyBytes { get; } = (int)crypto_sign_publickeybytes();

    public static int SecretKeyBytes { get; } = (int)crypto_sign_secretkeybytes();

    public static int SignatureBytes { get; } = (int)crypto_sign_bytes();

    public static int SeedBytes { get; } = (int)crypto_sign_seedbytes();

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

    public static void Sign(
        Span<byte> signedMessage,
        ReadOnlySpan<byte> message,
        ReadOnlySpan<byte> secretKey,
        out int signedMessageLength
    )
    {
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);
        Validate.GreaterOrEqualTo(signedMessage.Length, SignatureBytes + message.Length);

        var mlen = (ulong)message.Length;
        crypto_sign(signedMessage, out var smlen, message, mlen, secretKey).EnsureSuccess();
        signedMessageLength = (int)smlen;
    }

    public static bool Verify(
        Span<byte> message,
        ReadOnlySpan<byte> signedMessage,
        ReadOnlySpan<byte> publicKey,
        out int messageLength
    )
    {
        var smlen = (ulong)signedMessage.Length;
        var exitCode = crypto_sign_open(message, out var mlen, signedMessage, smlen, publicKey);
        messageLength = (int)mlen;
        return exitCode == 0;
    }

    public static void SignDetached(
        Span<byte> signature,
        ReadOnlySpan<byte> message,
        ReadOnlySpan<byte> secretKey,
        out int signatureLength
    )
    {
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        var mlen = (ulong)message.Length;
        crypto_sign_detached(signature, out var sigLen, message, mlen, secretKey).EnsureSuccess();
        signatureLength = (int)sigLen;
    }

    public static bool VerifyDetached(
        ReadOnlySpan<byte> signature,
        ReadOnlySpan<byte> message,
        ReadOnlySpan<byte> publicKey
    )
    {
        Validate.GreaterOrEqualTo(signature.Length, SignatureBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);

        var mlen = (ulong)message.Length;
        var exitCode = crypto_sign_verify_detached(signature, message, mlen, publicKey);
        return exitCode == 0;
    }

    public static IncrementalSignature Create()
    {
        var incrementalSignature = new IncrementalSignature();
        incrementalSignature.Initialize();
        return incrementalSignature;
    }
}
