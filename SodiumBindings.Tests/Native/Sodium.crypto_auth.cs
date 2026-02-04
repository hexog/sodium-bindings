namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoAuthBytes()
    {
        await Assert.That(crypto_auth_bytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoAuthKeybytes()
    {
        await Assert.That(crypto_auth_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoAuthPrimitive()
    {
        await Assert.That(crypto_auth_primitive()).IsEqualTo("hmacsha512256");
    }
}
