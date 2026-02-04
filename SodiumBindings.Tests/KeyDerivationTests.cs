namespace SodiumBindings.Tests;

public class KeyDerivationTests
{
    private static ReadOnlySpan<byte> Salt => "719af97d-b293-49fb-8ac7-eae5f71f7cd0"u8;

    private static ReadOnlySpan<byte> InputKeyingMaterial => "Hello, world!"u8;

    private static readonly byte[] ExpectedKey = Convert.FromHexString("247b297f6a7a11fa3ef6e6d33dbbb5e82f8d30b44fc44935e4d8883f7171e009");

    [Test]
    public async Task Extract()
    {
        var actualKey = new byte[32];
        KeyDerivation.Extract(actualKey, Salt, InputKeyingMaterial);
        await Assert.That(actualKey.AsSpan().SequenceEqual(ExpectedKey)).IsTrue();
    }

    [Test]
    public async Task IncrementalExtract()
    {
        var actualKey = new byte[32];

        var extractor = KeyDerivation.CreateExtractor(Salt);
        extractor.Update(InputKeyingMaterial[..5]);
        extractor.Update(InputKeyingMaterial[5..]);
        extractor.Final(actualKey);

        await Assert.That(actualKey.AsSpan().SequenceEqual(ExpectedKey)).IsTrue();
    }

    [Test]
    public async Task Derive()
    {
        var actualKey = new byte[32];
        KeyDerivation.Derive(actualKey, 10, "sdmbndgs"u8, ExpectedKey);
        var expectedKey = Convert.FromHexString("a336219fda10398f0955d2490415e2040222e41062fadd1b020ac4f597324183");
        await Assert.That(actualKey.AsSpan().SequenceEqual(expectedKey)).IsTrue();
    }

    [Test]
    public async Task HmacDerive()
    {
        var actualKey = new byte[32];
        KeyDerivation.HmacDerive(actualKey, "7e6e8059-ecc9-4272-90de-6c3d79aade7c"u8, ExpectedKey);
        var expectedKey = Convert.FromHexString("af4a04716f5fbe636cd500fbbe32dce6620a7dddfa5ae32e4211e84199a5e624");
        await Assert.That(actualKey.AsSpan().SequenceEqual(expectedKey)).IsTrue();
    }
}
