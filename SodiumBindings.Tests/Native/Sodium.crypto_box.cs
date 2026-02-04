namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoBosSeedbytes()
    {
        await Assert.That(crypto_box_seedbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoBoxPublickeybytes()
    {
        await Assert.That(crypto_box_publickeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoBoxSecretkeybytes()
    {
        await Assert.That(crypto_box_secretkeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoBoxNoncebytes()
    {
        await Assert.That(crypto_box_noncebytes()).IsEqualTo(24u);
    }

    [Test]
    public async Task CryptoBoxMacbytes()
    {
        await Assert.That(crypto_box_macbytes()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoBoxMessagebytesMax()
    {
        await Assert.That(crypto_box_messagebytes_max()).IsEqualTo((nuint)18446744073709551599UL);
    }

    [Test]
    public async Task CryptoBoxPrimitive()
    {
        await Assert.That(crypto_box_primitive()).IsEqualTo("curve25519xsalsa20poly1305");
    }

    [Test]
    public async Task CryptoBoxBeforenmbytes()
    {
        await Assert.That(crypto_box_beforenmbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoBoxSealbytes()
    {
        await Assert.That(crypto_box_sealbytes()).IsEqualTo(48u);
    }

    [Test]
    public async Task CryptoBoxZerobytes()
    {
        await Assert.That(crypto_box_zerobytes()).IsEqualTo(32u);
    }
}
