using System.Security.Cryptography;

namespace SodiumBindings;

public sealed class IncrementalSignature : IDisposable
{
    private readonly byte[] state = new byte[crypto_sign_statebytes()];

    internal IncrementalSignature()
    {
    }

    public void Initialize()
    {
        crypto_sign_init(state).EnsureSuccess();
    }

    public void Update(ReadOnlySpan<byte> data)
    {
        var dlen = (ulong)data.Length;
        crypto_sign_update(state, data, dlen).EnsureSuccess();
    }

    public void Create(Span<byte> signature, ReadOnlySpan<byte> secretKey, out int signatureLength)
    {
        Validate.GreaterOrEqualTo(signature.Length, Signature.SignatureBytes);
        Validate.GreaterOrEqualTo(secretKey.Length, Signature.SecretKeyBytes);

        crypto_sign_final_create(state, signature, out var siglen, secretKey).EnsureSuccess();
        signatureLength = (int)siglen;
    }

    public bool Verify(ReadOnlySpan<byte> signature, ReadOnlySpan<byte> publicKey)
    {
        Validate.GreaterOrEqualTo(signature.Length, Signature.SignatureBytes);
        Validate.GreaterOrEqualTo(publicKey.Length, Signature.PublicKeyBytes);

        var exitCode = crypto_sign_final_verify(state, signature, publicKey);
        return exitCode == 0;
    }

    public void Dispose()
    {
        CryptographicOperations.ZeroMemory(state);
    }
}
