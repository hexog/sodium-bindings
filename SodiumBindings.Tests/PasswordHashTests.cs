using System.Text;

namespace SodiumBindings.Tests;

public class PasswordHashTests
{
    private static ReadOnlySpan<byte> Password => "Hello, world!"u8;

    private static readonly byte[] Salt = new Guid("63ff237b-4c3d-4039-a703-22c34014a819").ToByteArray();

    private static readonly byte[] ExpectedHash = Convert.FromHexString("4ffd980fe1240c1e4eb5be2bba35043d409447f766667e6b0f915c3ecbc3caaf");

    [Test]
    public async Task Hash()
    {
        var actual = new byte[32];
        PasswordHash.Hash(actual, Password, Salt, operationsLimit: 1, memoryLimit: 8192u, PasswordHashAlgorithm.Argon2id13);
        await Assert.That(actual.AsSpan().SequenceEqual(ExpectedHash)).IsTrue();
    }

    [Test]
    public async Task HashToString()
    {
        var actual = new byte[PasswordHash.StringBytes];
        PasswordHash.HashToString(actual, Password, operationsLimit: 1, memoryLimit: 8192u);
        await Assert.That(Encoding.ASCII.GetString(actual)).Matches(@"^\$(?<alg>.+)\$v=\d+\$m=\d+,t=\d+,p=\d+\$(?<hash>.+)$");
        TestContext.Current!.StateBag.Items["HashString"] = actual;
    }

    [Test]
    [DependsOn(nameof(HashToString))]
    public async Task Verify()
    {
        var str = (byte[])TestContext.Current!.Dependencies.GetTests(nameof(HashToString))[0].StateBag["HashString"]!;
        var actual = PasswordHash.VerifyString(str, Password);
        await Assert.That(actual).IsTrue();
    }
}
