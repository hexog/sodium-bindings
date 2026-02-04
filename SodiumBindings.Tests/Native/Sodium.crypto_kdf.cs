namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoKdfBytesMin()
    {
        await Assert.That(crypto_kdf_bytes_min()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoKdfBytesMax()
    {
        await Assert.That(crypto_kdf_bytes_max()).IsEqualTo(64u);
    }

    [Test]
    public async Task CryptoKdfContextbytes()
    {
        await Assert.That(crypto_kdf_contextbytes()).IsEqualTo(8u);
    }

    [Test]
    public async Task CryptoKdfKeybytes()
    {
        await Assert.That(crypto_kdf_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKdfPrimitive()
    {
        await Assert.That(crypto_kdf_primitive()).IsEqualTo("blake2b");
    }
}
