namespace SodiumBindings.Tests;

public class PaddingTests
{
    [Test]
    public async Task Pad()
    {
        var buffer = new byte[8];
        "Hello"u8.CopyTo(buffer);

        var paddedLength = Padding.Pad(buffer, 5, 8);
        byte[] expected = [.. "Hello"u8, 0x80, 0, 0];

        await Assert.That(paddedLength).IsEqualTo(8);
        await Assert.That(buffer.AsSpan().SequenceEqual(expected)).IsTrue();
    }

    [Test]
    public async Task PadAlignedInputAddsFullBlock()
    {
        var buffer = new byte[16];
        "12345678"u8.CopyTo(buffer);

        var paddedLength = Padding.Pad(buffer, 8, 8);

        await Assert.That(paddedLength).IsEqualTo(16);
        await Assert.That(buffer[8]).IsEqualTo((byte)0x80);
        await Assert.That(buffer.AsSpan(9).IndexOfAnyExcept((byte)0)).IsEqualTo(-1);
    }

    [Test]
    public async Task Unpad()
    {
        var buffer = new byte[8];
        "Hello"u8.CopyTo(buffer);
        var paddedLength = Padding.Pad(buffer, 5, 8);

        var success = Padding.Unpad(buffer.AsSpan(0, paddedLength), 8, out var unpaddedLength);

        await Assert.That(success).IsTrue();
        await Assert.That(unpaddedLength).IsEqualTo(5);
        await Assert.That(buffer.AsSpan(0, unpaddedLength).SequenceEqual("Hello"u8)).IsTrue();
    }

    [Test]
    public async Task UnpadInvalidPadding()
    {
        byte[] buffer = [.. "Hello"u8, 0x80, 0, 1];

        var success = Padding.Unpad(buffer, 8, out var unpaddedLength);

        await Assert.That(success).IsFalse();
        await Assert.That(unpaddedLength).IsEqualTo(0);
    }

    [Test]
    public async Task UnpadTruncatedBlock()
    {
        var success = Padding.Unpad(new byte[7], 8, out var unpaddedLength);

        await Assert.That(success).IsFalse();
        await Assert.That(unpaddedLength).IsEqualTo(0);
    }

    [Test]
    public async Task PadInsufficientCapacity()
    {
        var buffer = new byte[7];
        "Hello"u8.CopyTo(buffer);

        await Assert.That(() => Padding.Pad(buffer, 5, 8)).Throws<SodiumException>();
    }

    [Test]
    public async Task PadInvalidUnpaddedLength()
    {
        var buffer = new byte[8];

        await Assert.That(() => Padding.Pad(buffer, 9, 8)).Throws<SodiumException>();
    }

    [Test]
    public async Task PadInvalidBlockSize()
    {
        var buffer = new byte[8];

        await Assert.That(() => Padding.Pad(buffer, 0, 0)).Throws<SodiumException>();
    }

    [Test]
    public async Task UnpadInvalidBlockSize()
    {
        var buffer = new byte[8];

        await Assert.That(() => Padding.Unpad(buffer, -1, out _)).Throws<SodiumException>();
    }
}
