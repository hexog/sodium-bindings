namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task SodiumVersionString()
    {
        await Assert.That(sodium_version_string()).IsEqualTo("1.0.21");
    }

    [Test]
    public async Task SodiumLibraryVersionMajor()
    {
        await Assert.That(sodium_library_version_major()).IsEqualTo(26);
    }

    [Test]
    public async Task SodiumLibraryVersionMinor()
    {
        await Assert.That(sodium_library_version_minor()).IsEqualTo(3);
    }

    [Test]
    public async Task SodiumLibraryMinimal()
    {
        await Assert.That(sodium_library_minimal()).IsEqualTo(0);
    }
}
