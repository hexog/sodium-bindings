namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoSecretboxKeybytes()
    {
        await Assert.That(crypto_secretbox_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoSecretboxNoncebytes()
    {
        await Assert.That(crypto_secretbox_noncebytes()).IsEqualTo(24u);
    }

    [Test]
    public async Task CryptoSecretboxMacbytes()
    {
        await Assert.That(crypto_secretbox_macbytes()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoSecretboxMessagebytesMax()
    {
        await Assert.That(crypto_secretbox_messagebytes_max()).IsEqualTo((nuint)18446744073709551599UL);
    }

    [Test]
    public async Task CryptoSecretboxZerobytes()
    {
        await Assert.That(crypto_secretbox_zerobytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoSecretboxBoxzerobytes()
    {
        await Assert.That(crypto_secretbox_boxzerobytes()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoSecretboxPrimitive()
    {
        await Assert.That(crypto_secretbox_primitive()).IsEqualTo("xsalsa20poly1305");
    }
}
