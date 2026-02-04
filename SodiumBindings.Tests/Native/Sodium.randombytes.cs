namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task RandombytesSeedbytes()
    {
        await Assert.That(randombytes_seedbytes()).IsEqualTo(32u);
    }
}
