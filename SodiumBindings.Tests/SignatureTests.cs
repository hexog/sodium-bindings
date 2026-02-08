namespace SodiumBindings.Tests;

public class SignatureTests
{
    private static readonly byte[] SecretKey =
        Convert.FromHexString(
            "0fbd4e874817fc87d0be7ec737903469e49f737da1e48ef605d191250ee7a9e2149c09c619789ed4366628192eaba7737cb198fcdb71a42a13a944a10fd6c386");

    private static readonly byte[] PublicKey = Convert.FromHexString("149c09c619789ed4366628192eaba7737cb198fcdb71a42a13a944a10fd6c386");

    private static readonly byte[] ExpectedSignature =
        Convert.FromHexString(
            "fc23d3266634b776579b1a8accd10b2d969776df4d4d1e91506e2e45ce1f0213a42b9f76180540ebe6404684423e669029318da1fb0c83d7068743f842a8500a");

    private static ReadOnlySpan<byte> Message => "Hello, world!"u8;

    [Test]
    public async Task Sign()
    {
        var actualSignature = new byte[Signature.SignatureBytes];
        Signature.SignDetached(actualSignature, Message, SecretKey);
        await Assert.That(actualSignature.AsSpan().SequenceEqual(ExpectedSignature)).IsTrue();
    }

    [Test]
    public async Task Verify()
    {
        var actual = Signature.VerifyDetached(ExpectedSignature, Message, PublicKey);
        await Assert.That(actual).IsTrue();
    }
}
