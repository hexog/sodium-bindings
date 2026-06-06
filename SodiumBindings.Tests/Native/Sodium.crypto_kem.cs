namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoKemPublickeybytes()
    {
        await Assert.That(crypto_kem_publickeybytes()).IsEqualTo(1216u);
    }

    [Test]
    public async Task CryptoKemSecretkeybytes()
    {
        await Assert.That(crypto_kem_secretkeybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKemCiphertextbytes()
    {
        await Assert.That(crypto_kem_ciphertextbytes()).IsEqualTo(1120u);
    }

    [Test]
    public async Task CryptoKemSharedsecretbytes()
    {
        await Assert.That(crypto_kem_sharedsecretbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKemSeedbytes()
    {
        await Assert.That(crypto_kem_seedbytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKemPrimitive()
    {
        await Assert.That(crypto_kem_primitive()).IsEqualTo("xwing");
    }
}
