namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoSignStatebytes()
    {
        await Assert.That(crypto_sign_statebytes()).IsEqualTo(208u);
    }

    [Test]
    public async Task CryptoSignBytes()
    {
        await Assert.That(crypto_sign_bytes()).IsEqualTo(64u);
    }

    [Test]
    public async Task CryptoSignSeedbytes()
    {
        await Assert.That(crypto_sign_seedbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoSignPublickeybytes()
    {
        await Assert.That(crypto_sign_publickeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoSignSecretkeybytes()
    {
        await Assert.That(crypto_sign_secretkeybytes()).IsEqualTo(64u);
    }

    [Test]
    public async Task CryptoSignMessagebytesMax()
    {
        await Assert.That(crypto_sign_messagebytes_max()).IsEqualTo((nuint)18446744073709551551UL);
    }

    [Test]
    public async Task CryptoSignPrimitive()
    {
        await Assert.That(crypto_sign_primitive()).IsEqualTo("ed25519");
    }
}
