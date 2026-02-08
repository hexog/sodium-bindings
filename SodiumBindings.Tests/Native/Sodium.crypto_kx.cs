namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoKxPublickeybytes()
    {
        await Assert.That(crypto_kx_publickeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKxSecretkeybytes()
    {
        await Assert.That(crypto_kx_secretkeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKxSeedbytes()
    {
        await Assert.That(crypto_kx_seedbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKxSessionkeybytes()
    {
        await Assert.That(crypto_kx_sessionkeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKxPrimitive()
    {
        await Assert.That(crypto_kx_primitive()).IsEqualTo("x25519blake2b");
    }
}
