using SodiumBindings.Aead;

namespace SodiumBindings.Tests.Aead;

public class Aegis256Tests
{
    private static readonly byte[] Key = Convert.FromHexString("a4559fccd62cb8290e6964520330badb13db17fe3b370541eb84f4e1bce2795a");

    private static readonly byte[] Nonce = Convert.FromHexString("01c34c04668360bdf1b20ae635eb7ed36053aad5e0ef0f407221bf62d20944f3");

    private static readonly byte[] ExpectedCiphertext =
        Convert.FromHexString("9feb17c3783b89fcc1b2fd742e85a1fbc7540dec17198d9aac9d501721f94a33cd025c68b83c3f2249ca15c0a7");

    private static readonly byte[] ExpectedCiphertextWithAdditionalData =
        Convert.FromHexString("759100dc1db3176698151a39fa90473ddb7f74c4ab13130e69f7f41ed3fb1a6ccc02bf641dd8ce4663fdd8c078");

    private static ReadOnlySpan<byte> Message => "Hello, world!"u8;

    private static ReadOnlySpan<byte> AdditionalData => "Additional data"u8;

    [Test]
    public async Task Encrypt()
    {
        var actualCiphertext = new byte[Aegis256.GetCiphertextLength((ulong)Message.Length)];
        Aegis256.Encrypt(
            actualCiphertext,
            Message,
            null,
            Nonce, Key
        );

        await Assert.That(actualCiphertext.AsSpan().SequenceEqual(ExpectedCiphertext)).IsTrue();
    }

    [Test]
    public async Task EncryptWithAdditionalData()
    {
        var actualCiphertext = new byte[Aegis256.GetCiphertextLength((ulong)Message.Length)];
        Aegis256.Encrypt(
            actualCiphertext,
            Message,
            AdditionalData,
            Nonce, Key
        );

        await Assert.That(actualCiphertext.AsSpan().SequenceEqual(ExpectedCiphertextWithAdditionalData)).IsTrue();
    }

    [Test]
    public async Task Decrypt()
    {
        var actualPlaintext = new byte[Aegis256.GetPlaintextLength((ulong)ExpectedCiphertext.Length)];
        var actual = Aegis256.Decrypt(
            actualPlaintext,
            ExpectedCiphertext,
            null,
            Nonce, Key
        );

        await Assert.That(actual).IsTrue();
        await Assert.That(actualPlaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }

    [Test]
    public async Task DecryptWithAdditionalData()
    {
        var actualPlaintext = new byte[Aegis256.GetPlaintextLength((ulong)ExpectedCiphertextWithAdditionalData.Length)];
        var actual = Aegis256.Decrypt(
            actualPlaintext,
            ExpectedCiphertextWithAdditionalData,
            AdditionalData,
            Nonce, Key
        );

        await Assert.That(actual).IsTrue();
        await Assert.That(actualPlaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }

    [Test]
    public async Task DecryptFailed()
    {
        var actualPlaintext = new byte[Aegis256.GetPlaintextLength((ulong)ExpectedCiphertext.Length)];
        var tamperedCiphertext = ExpectedCiphertext.ToArray();
        tamperedCiphertext[0] = 0;
        var actual = Aegis256.Decrypt(
            actualPlaintext,
            tamperedCiphertext,
            null,
            Nonce, Key
        );

        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task DecryptWithAdditionalDataFailed()
    {
        var actualPlaintext = new byte[Aegis256.GetPlaintextLength((ulong)ExpectedCiphertextWithAdditionalData.Length)];
        var tamperedCiphertext = ExpectedCiphertextWithAdditionalData.ToArray();
        tamperedCiphertext[0] = 0;
        var actual = Aegis256.Decrypt(
            actualPlaintext,
            tamperedCiphertext,
            AdditionalData,
            Nonce, Key
        );

        await Assert.That(actual).IsFalse();
    }

    [Test]
    public async Task DecryptWithTamperedAdditionalDataFailed()
    {
        var actualPlaintext = new byte[Aegis256.GetPlaintextLength((ulong)ExpectedCiphertextWithAdditionalData.Length)];
        var tamperedAdditionalData = AdditionalData.ToArray();
        tamperedAdditionalData[0] = 0;
        var actual = Aegis256.Decrypt(
            actualPlaintext,
            ExpectedCiphertextWithAdditionalData,
            tamperedAdditionalData,
            Nonce, Key
        );

        await Assert.That(actual).IsFalse();
    }
    [Test]
    public async Task DecryptWithTamperedAdditionalDataAddCiphertextFailed()
    {
        var actualPlaintext = new byte[Aegis256.GetPlaintextLength((ulong)ExpectedCiphertextWithAdditionalData.Length)];
        var tamperedCiphertext = ExpectedCiphertextWithAdditionalData.ToArray();
        tamperedCiphertext[0] = 0;
        var tamperedAdditionalData = AdditionalData.ToArray();
        tamperedAdditionalData[0] = 0;
        var actual = Aegis256.Decrypt(
            actualPlaintext,
            tamperedCiphertext,
            tamperedAdditionalData,
            Nonce, Key
        );

        await Assert.That(actual).IsFalse();
    }
}
