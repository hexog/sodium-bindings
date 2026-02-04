namespace SodiumBindings.Tests;

public class GenericHashTests
{
    private static readonly byte[] ExpectedBytes = Convert.FromHexString("b5da441cfe72ae042ef4d2b17742907f675de4da57462d4c3609c2e2ed755970");

    private static ReadOnlySpan<byte> Message => "Hello, world!"u8;

    [Test]
    public async Task Hash()
    {
        var actualBytes = new byte[32];
        GenericHash.Hash(actualBytes, Message);
        await Assert.That(actualBytes.AsSpan().SequenceEqual(ExpectedBytes)).IsTrue();
    }

    [Test]
    public async Task IncrementalHash()
    {
        var actualBytes = new byte[32];
        var hash = GenericHash.Create(32);

        hash.Update(Message[..5]);
        hash.Update(Message[5..]);
        hash.Final(actualBytes);

        await Assert.That(actualBytes.AsSpan().SequenceEqual(ExpectedBytes)).IsTrue();
    }
}
