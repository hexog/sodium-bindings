namespace SodiumBindings.Tests;

public class SecretBoxTests
{
    private static readonly byte[] Key = Convert.FromHexString("4338aad987546f393c5c8d12a5140fae1c52dfaf167e48e1a9b59b77cf053715");
    private static readonly byte[] Nonce = Convert.FromHexString("d7c142164898f7682f82977a1d29f91db507a683a2692c1c");
    private static readonly byte[] ExpectedCiphertext = Convert.FromHexString("3406e0d1b1d0714a5b6db43e487cf168d56282fd6fd312ffd054fc812e");

    private static ReadOnlySpan<byte> Message => "Hello, world!"u8;

    [Test]
    public async Task Encrypt()
    {
        var actualCiphertext = new byte[(ulong)Message.Length + SecretBox.MacBytes];
        SecretBox.Encrypt(actualCiphertext, Message, Nonce, Key);
        await Assert.That(actualCiphertext.AsSpan().SequenceEqual(ExpectedCiphertext)).IsTrue();
    }

    [Test]
    public async Task Decrypt()
    {
        var actualPlaintext = new byte[(ulong)ExpectedCiphertext.Length - SecretBox.MacBytes];
        SecretBox.Decrypt(actualPlaintext, ExpectedCiphertext, Nonce, Key);
        await Assert.That(actualPlaintext.AsSpan().SequenceEqual(Message)).IsTrue();
    }
}
