namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoStreamKeybytes()
    {
        await Assert.That(crypto_stream_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoStreamNoncebytes()
    {
        await Assert.That(crypto_stream_noncebytes()).IsEqualTo(24u);
    }

    [Test]
    public async Task CryptoStreamMessagebytesMax()
    {
        await Assert.That(crypto_stream_messagebytes_max()).IsEqualTo((nuint)18446744073709551615UL);
    }

    [Test]
    public async Task CryptoStreamPrimitive()
    {
        await Assert.That(crypto_stream_primitive()).IsEqualTo("xsalsa20");
    }
}
