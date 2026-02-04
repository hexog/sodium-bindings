namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoGenerichashBytesMin()
    {
        await Assert.That(crypto_generichash_bytes_min()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoGenerichashBytesMax()
    {
        await Assert.That(crypto_generichash_bytes_max()).IsEqualTo(64u);
    }

    [Test]
    public async Task CryptoGenerichashBytes()
    {
        await Assert.That(crypto_generichash_bytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoGenerichashKeybytesMin()
    {
        await Assert.That(crypto_generichash_keybytes_min()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoGenerichashKeybytesMax()
    {
        await Assert.That(crypto_generichash_keybytes_max()).IsEqualTo(64u);
    }

    [Test]
    public async Task CryptoGenerichashKeybytes()
    {
        await Assert.That(crypto_generichash_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoGenerichashPrimitive()
    {
        await Assert.That(crypto_generichash_primitive()).IsEqualTo("blake2b");
    }

    [Test]
    public async Task CryptoGenerichashStatebytes()
    {
        await Assert.That(crypto_generichash_statebytes()).IsEqualTo(GenericHash.StateBytes);
    }
}
