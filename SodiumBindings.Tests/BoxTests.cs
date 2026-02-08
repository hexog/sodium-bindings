namespace SodiumBindings.Tests;

public class BoxTests
{
    private static readonly byte[] Seed = Convert.FromHexString("f8b4fb3a21bc75141e0cb804c8d91b2137a69910c089a3f899ed669d3bee44a2");

    private static readonly byte[] AlicePublicKey = Convert.FromHexString("8fd9fc3451711c41fbc628b2ff1a997f0aeed613a74f4181c570cac232c97434");

    private static readonly byte[] AliceSecretKey = Convert.FromHexString("625598fe7205b3aa25b6e4566e41d1b50b2f9a2a3cc183738010216218c620a0");

    private static readonly byte[] BobPublicKey = Convert.FromHexString("f35e76acb7073d79f4f3cf72ce43d64ac51410784d05ccd4fd33cbdd86796709");

    private static readonly byte[] BobSecretKey = Convert.FromHexString("02fb1e8b4d16683774862b2ef59a4018f2665f1fca73606e9a39646c70443366");

    private static readonly byte[] Nonce = Convert.FromHexString("1209bcf5a41301077aaa9c81c87ac30227080ac3be644d1a");

    private static readonly byte[] ExpectedCiphertext = Convert.FromHexString("c5bed75ca161e02eec5d1951844ab91ea262a3c6ee415253bbe70de7b6");

    private static readonly byte[] MacOnly = Convert.FromHexString("c5bed75ca161e02eec5d1951844ab91e");

    private static readonly byte[] CiphertextOnly = Convert.FromHexString("a262a3c6ee415253bbe70de7b6");

    private static readonly byte[] PrecalculatedKey = Convert.FromHexString("d741547aea5cb6b1fc34126fb380644b6f71d7b08c0419acd95f3faebe681bc5");

    private static ReadOnlySpan<byte> Message => "Hello, world!"u8;

    [Test]
    public async Task GenerateKeyPair()
    {
        var publicKey = new byte[Box.PublicKeyBytes];
        var secretKey = new byte[Box.SecretKeyBytes];

        Box.GenerateKeyPair(publicKey, secretKey);
    }

    [Test]
    public async Task GenerateKeyPairFromSeed()
    {
        var publicKey = new byte[Box.PublicKeyBytes];
        var secretKey = new byte[Box.SecretKeyBytes];
        Box.GenerateKeyPairFromSeed(publicKey, secretKey, Seed);

        await Assert.That(publicKey.AsSpan().SequenceEqual(AlicePublicKey)).IsTrue();
        await Assert.That(secretKey.AsSpan().SequenceEqual(AliceSecretKey)).IsTrue();
    }

    [Test]
    public async Task Encrypt()
    {
        var ciphertext = new byte[Box.GetCiphertextLength(Message.Length)];
        Box.Encrypt(ciphertext, Message, Nonce, BobPublicKey, AliceSecretKey);
        await Assert.That(ciphertext.AsSpan().SequenceEqual(ExpectedCiphertext)).IsTrue();
    }

    [Test]
    public async Task Decrypt()
    {
        var plaintext = new byte[Box.GetPlaintextLength(ExpectedCiphertext.Length)];
        Box.Decrypt(plaintext, ExpectedCiphertext, Nonce, AlicePublicKey, BobSecretKey);
        await Assert.That(plaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }

    [Test]
    public async Task EncryptDetached()
    {
        var ciphertext = new byte[Message.Length];
        var mac = new byte[Box.MacBytes];
        Box.EncryptDetached(ciphertext, mac, Message, Nonce, BobPublicKey, AliceSecretKey);
        await Assert.That(ciphertext.AsSpan().SequenceEqual(CiphertextOnly)).IsTrue();
        await Assert.That(mac.AsSpan().SequenceEqual(MacOnly)).IsTrue();
    }

    [Test]
    public async Task DecryptDetached()
    {
        var plaintext = new byte[Box.GetPlaintextLength(ExpectedCiphertext.Length)];
        Box.DecryptDetached(plaintext, CiphertextOnly, MacOnly, Nonce, AlicePublicKey, BobSecretKey);
        await Assert.That(plaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }

    [Test]
    public async Task GeneratedPrecalculatedKey()
    {
        var key = new byte[Box.PrecalculatedKeyBytes];
        Box.GeneratedPrecalculatedKey(key, BobPublicKey, AliceSecretKey);
        await Assert.That(key.AsSpan().SequenceEqual(PrecalculatedKey)).IsTrue();
    }

    [Test]
    public async Task EncryptPrecalculated()
    {
        var ciphertext = new byte[Box.GetCiphertextLength(Message.Length)];
        Box.EncryptPrecalculated(ciphertext, Message, Nonce, PrecalculatedKey);
        await Assert.That(ciphertext.AsSpan().SequenceEqual(ExpectedCiphertext)).IsTrue();
    }

    [Test]
    public async Task DecryptPrecalculated()
    {
        var plaintext = new byte[Box.GetPlaintextLength(ExpectedCiphertext.Length)];
        Box.DecryptPrecalculated(plaintext, ExpectedCiphertext, Nonce, PrecalculatedKey);
        await Assert.That(plaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }

    [Test]
    public async Task EncryptPrecalculatedDetached()
    {
        var ciphertext = new byte[Message.Length];
        var mac = new byte[Box.MacBytes];
        Box.EncryptPrecalculatedDetached(ciphertext, mac, Message, Nonce, PrecalculatedKey);
        await Assert.That(ciphertext.AsSpan().SequenceEqual(CiphertextOnly)).IsTrue();
        await Assert.That(mac.AsSpan().SequenceEqual(MacOnly)).IsTrue();
    }

    [Test]
    public async Task DecryptPrecalculatedDetached()
    {
        var plaintext = new byte[Box.GetPlaintextLength(ExpectedCiphertext.Length)];
        Box.DecryptPrecalculatedDetached(plaintext, CiphertextOnly, MacOnly, Nonce, PrecalculatedKey);
        await Assert.That(plaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }
}
