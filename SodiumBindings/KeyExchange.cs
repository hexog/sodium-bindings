namespace SodiumBindings;

public static class KeyExchange
{
    public static ulong PublicKeyBytes => crypto_kx_publickeybytes();

    public static ulong SecretKeyBytes => crypto_kx_secretkeybytes();

    public static ulong SeedBytes => crypto_kx_seedbytes();

    public static ulong SessionKeyBytes => crypto_kx_sessionkeybytes();

    public static void GenerateKeyPair(
        Span<byte> publicKey,
        Span<byte> secretKey
    )
    {
        Validate.GreaterOrEqualTo(publicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, SecretKeyBytes);

        crypto_kx_keypair(publicKey, secretKey).EnsureSuccess();
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

        crypto_kx_seed_keypair(publicKey, secretKey, seed).EnsureSuccess();
    }

    public static void ComputeClientSessionKeys(
        Span<byte> receiveKey,
        Span<byte> transmitKey,
        ReadOnlySpan<byte> clientPublicKey,
        ReadOnlySpan<byte> clientSecretKey,
        ReadOnlySpan<byte> serverPublicKey
    )
    {
        if (!receiveKey.IsEmpty)
        {
            Validate.GreaterOrEqualTo(receiveKey.Length, SessionKeyBytes);
        }

        if (!transmitKey.IsEmpty)
        {
            Validate.GreaterOrEqualTo(transmitKey.Length, SessionKeyBytes);
        }

        Validate.GreaterOrEqualTo(clientPublicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(clientSecretKey.Length, SecretKeyBytes);
        Validate.GreaterOrEqualTo(serverPublicKey.Length, PublicKeyBytes);

        crypto_kx_client_session_keys(receiveKey, transmitKey, clientPublicKey, clientSecretKey, serverPublicKey).EnsureSuccess();
    }

    public static void ComputeServerSessionKeys(
        Span<byte> receiveKey,
        Span<byte> transmitKey,
        ReadOnlySpan<byte> serverPublicKey,
        ReadOnlySpan<byte> serverSecretKey,
        ReadOnlySpan<byte> clientPublicKey
    )
    {
        if (!receiveKey.IsEmpty)
        {
            Validate.GreaterOrEqualTo(receiveKey.Length, SessionKeyBytes);
        }

        if (!transmitKey.IsEmpty)
        {
            Validate.GreaterOrEqualTo(transmitKey.Length, SessionKeyBytes);
        }

        Validate.GreaterOrEqualTo(serverPublicKey.Length, PublicKeyBytes);
        Validate.GreaterOrEqualTo(serverSecretKey.Length, SecretKeyBytes);
        Validate.GreaterOrEqualTo(clientPublicKey.Length, PublicKeyBytes);

        crypto_kx_server_session_keys(receiveKey, transmitKey, serverPublicKey, serverSecretKey, clientPublicKey).EnsureSuccess();
    }
}
