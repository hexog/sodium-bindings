namespace SodiumBindings.Tests.Native;

public partial class Sodium
{
    [Test]
    public async Task CryptoPwhashAlgArgon2I13()
    {
        await Assert.That(crypto_pwhash_alg_argon2i13()).IsEqualTo(1);
    }

    [Test]
    public async Task CryptoPwhashAlgArgon2Id13()
    {
        await Assert.That(crypto_pwhash_alg_argon2id13()).IsEqualTo(2);
    }

    [Test]
    public async Task CryptoPwhashAlgDefault()
    {
        await Assert.That(crypto_pwhash_alg_default()).IsEqualTo(2);
    }

    [Test]
    public async Task CryptoPwhashBytesMin()
    {
        await Assert.That(crypto_pwhash_bytes_min()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoPwhashBytesMax()
    {
        await Assert.That(crypto_pwhash_bytes_max()).IsEqualTo(4294967295u);
    }

    [Test]
    public async Task CryptoPwhashPasswdMin()
    {
        await Assert.That(crypto_pwhash_passwd_min()).IsEqualTo(0u);
    }

    [Test]
    public async Task CryptoPwhashPasswdMax()
    {
        await Assert.That(crypto_pwhash_passwd_max()).IsEqualTo(4294967295u);
    }

    [Test]
    public async Task CryptoPwhashSaltbytes()
    {
        await Assert.That(crypto_pwhash_saltbytes()).IsEqualTo(16u);
    }

    [Test]
    public async Task CryptoPwhashStrbytes()
    {
        await Assert.That(crypto_pwhash_strbytes()).IsEqualTo(128u);
    }

    [Test]
    public async Task CryptoPwhashOpslimitMin()
    {
        await Assert.That(crypto_pwhash_opslimit_min()).IsEqualTo(1u);
    }

    [Test]
    public async Task CryptoPwhashOpslimitMax()
    {
        await Assert.That(crypto_pwhash_opslimit_max()).IsEqualTo(4294967295u);
    }

    [Test]
    public async Task CryptoPwhashMemlimitMin()
    {
        await Assert.That(crypto_pwhash_memlimit_min()).IsEqualTo(8192u);
    }

    [Test]
    public async Task CryptoPwhashMemlimitMax()
    {
        await Assert.That(crypto_pwhash_memlimit_max()).IsEqualTo((nuint)4398046510080UL);
    }

    [Test]
    public async Task CryptoPwhashOpslimitInteractive()
    {
        await Assert.That(crypto_pwhash_opslimit_interactive()).IsEqualTo(2u);
    }

    [Test]
    public async Task CryptoPwhashMemlimitInteractive()
    {
        await Assert.That(crypto_pwhash_memlimit_interactive()).IsEqualTo(67108864u);
    }

    [Test]
    public async Task CryptoPwhashOpslimitModerate()
    {
        await Assert.That(crypto_pwhash_opslimit_moderate()).IsEqualTo(3u);
    }

    [Test]
    public async Task CryptoPwhashMemlimitModerate()
    {
        await Assert.That(crypto_pwhash_memlimit_moderate()).IsEqualTo(268435456u);
    }

    [Test]
    public async Task CryptoPwhashOpslimitSensitive()
    {
        await Assert.That(crypto_pwhash_opslimit_sensitive()).IsEqualTo(4u);
    }

    [Test]
    public async Task CryptoPwhashMemlimitSensitive()
    {
        await Assert.That(crypto_pwhash_memlimit_sensitive()).IsEqualTo(1073741824u);
    }

    [Test]
    public async Task CryptoPwhashStrprefix()
    {
        await Assert.That(crypto_pwhash_strprefix()).IsEqualTo("$argon2id$");
    }

    [Test]
    public async Task CryptoPwhashPrimitive()
    {
        await Assert.That(crypto_pwhash_primitive()).IsEqualTo("argon2id,argon2i");
    }
}
