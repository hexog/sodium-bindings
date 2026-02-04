namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoKdfHkdfSha256Keybytes()
    {
        await Assert.That(crypto_kdf_hkdf_sha256_keybytes()).IsEqualTo(32u);
    }

    [Test]
    public async Task CryptoKdfHkdfSha256BytesMin()
    {
        await Assert.That(crypto_kdf_hkdf_sha256_bytes_min()).IsEqualTo(0u);
    }

    [Test]
    public async Task CryptoKdfHkdfSha256BytesMax()
    {
        await Assert.That(crypto_kdf_hkdf_sha256_bytes_max()).IsEqualTo(8160u);
    }

    [Test]
    public async Task CryptoKdfHkdfSha256Statebytes()
    {
        await Assert.That(crypto_kdf_hkdf_sha256_statebytes()).IsEqualTo(KeyDerivation.StateBytes);
    }
}
