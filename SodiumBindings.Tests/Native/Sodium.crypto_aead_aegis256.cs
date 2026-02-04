namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoAeadAegis256Keybytes()
    {
        await Assert.That(crypto_aead_aegis256_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoAeadAegis256Nsecbytes()
    {
        await Assert.That(crypto_aead_aegis256_nsecbytes()).IsEqualTo(0u);
    }

    [Test]
    public async Task CryptoAeadAegis256Npubbytes()
    {
        await Assert.That(crypto_aead_aegis256_npubbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoAeadAegis256Abytes()
    {
        await Assert.That(crypto_aead_aegis256_abytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoAeadAegis256MessagebytesMax()
    {
        await Assert.That(crypto_aead_aegis256_messagebytes_max()).IsEqualTo((nuint)2305843009213693951UL);
    }
}
