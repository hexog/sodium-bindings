namespace SodiumBindings.Tests;

public class SignatureTests
{
    private static readonly byte[] Seed = Convert.FromHexString("46400a287dd1a13e168f02739648976803ccd5fa58d2483275ff4a3946f664ec");

    private static readonly byte[] SecretKey =
        Convert.FromHexString(
            "46400a287dd1a13e168f02739648976803ccd5fa58d2483275ff4a3946f664ecfe9f9d5c425c3f0d59b3e0b303db344b8a2b1e528e4fbaa9b42af3993ceaf607");

    private static readonly byte[] PublicKey = Convert.FromHexString("fe9f9d5c425c3f0d59b3e0b303db344b8a2b1e528e4fbaa9b42af3993ceaf607");

    private static readonly byte[] ExpectedSignature =
        Convert.FromHexString(
            "c5831d455fddd1a86d8e468851ba1bc8efd114aeb2feddff9d25f7c163e922621defec0d026ed8b1011dd6878da3be7e8c4cc7cff7ada1009d17aa0a7962ed00");

    private static readonly byte[] ExpectedMultipartSignature =
        Convert.FromHexString(
            "c985282565b0d4cd2a9884bf45289d771e6be387f752f94e801b17b7f2c0caecb2e02e3761b549cc648a687cdd9f775852fc5a9703de9df91220b3a9f0323906");

    private static ReadOnlySpan<byte> Message => "Hello, world!"u8;

    [Test]
    public void GenerateKeyPair()
    {
        var publicKey = new byte[Signature.PublicKeyBytes];
        var secretKey = new byte[Signature.SecretKeyBytes];
        Signature.GenerateKeyPair(publicKey, secretKey);
    }

    [Test]
    public async Task GenerateKeyPairFromSeed()
    {
        var publicKey = new byte[Signature.PublicKeyBytes];
        var secretKey = new byte[Signature.SecretKeyBytes];
        Signature.GenerateKeyPairFromSeed(publicKey, secretKey, Seed);
        await Assert.That(publicKey.AsSpan().SequenceEqual(PublicKey)).IsTrue();
        await Assert.That(secretKey.AsSpan().SequenceEqual(SecretKey)).IsTrue();
    }

    [Test]
    public async Task Sign()
    {
        var actualSignature = new byte[Signature.SignatureBytes];
        Signature.SignDetached(actualSignature, Message, SecretKey);
        await Assert.That(actualSignature.AsSpan().SequenceEqual(ExpectedSignature)).IsTrue();
    }

    [Test]
    public async Task IncrementalSign()
    {
        var actualSignature = new byte[Signature.SignatureBytes];
        using var incrementalSignature = Signature.Create();
        incrementalSignature.Update(Message[..4]);
        incrementalSignature.Update(Message[4..]);
        incrementalSignature.Create(actualSignature, SecretKey);
        await Assert.That(actualSignature.AsSpan().SequenceEqual(ExpectedMultipartSignature)).IsTrue();
    }

    [Test]
    public async Task Verify()
    {
        var actual = Signature.VerifyDetached(ExpectedSignature, Message, PublicKey);
        await Assert.That(actual).IsTrue();
    }

    [Test]
    public async Task IncrementalVerify()
    {
        using var incrementalSignature = Signature.Create();
        incrementalSignature.Update(Message[..3]);
        incrementalSignature.Update(Message[3..4]);
        incrementalSignature.Update(Message[4..]);
        var actual = incrementalSignature.Verify(ExpectedMultipartSignature, PublicKey);
        await Assert.That(actual).IsTrue();
    }
}
