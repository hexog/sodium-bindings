using System.Security.Cryptography;

namespace SodiumBindings.Tests;

public class KeyExchangeTests
{
    private static readonly byte[] Seed = Convert.FromHexString("0557bd52e26a65a7f0a5d6b8b96215d6c3697c6aaa74e0e34af3601aa509238a");

    private static readonly byte[] AlicePublicKey = Convert.FromHexString("5203550f97883f6536359be12dfdb22ef26c6833d9e6e97b98f27c9ce8cdaf6f");

    private static readonly byte[] AliceSecretKey = Convert.FromHexString("90637074f6d0c3b90f697523bb0c4ef016ba0100ae3b313e6eee8adf694676ef");

    private static readonly byte[] BobPublicKey = Convert.FromHexString("d035a00ac6aef1f68b59eae75dfab446c4af5dc6c682e03b02b14b63a84e3c77");

    private static readonly byte[] BobSecretKey = Convert.FromHexString("1b07fb4b1da8c9edd4ef7c28f1cdb2b44faabc72a0f887f3e5b5cb211e478263");

    private static readonly byte[] SessionKeyAliceToBob = Convert.FromHexString("6cccab2a5d45c56e828fc69652d4b83d63fe578b4e4c5bde9be18d9567e4194f");

    private static readonly byte[] SessionKeyBobToAlice  = Convert.FromHexString("48d861d73b3e23ce8e0147eae370f2cd87bfe743d47360321515a9ca05f803f8");

    [Test]
    public void GenerateKeyPair()
    {
        var publicKey = new byte[KeyExchange.PublicKeyBytes];
        var secretKey = new byte[KeyExchange.SecretKeyBytes];
        KeyExchange.GenerateKeyPair(publicKey, secretKey);
    }

    [Test]
    public async Task GenerateKeyPairFromSeed()
    {
        var publicKey = new byte[KeyExchange.PublicKeyBytes];
        var secretKey = new byte[KeyExchange.SecretKeyBytes];
        KeyExchange.GenerateKeyPairFromSeed(publicKey, secretKey, Seed);
        await Assert.That(publicKey.AsSpan().SequenceEqual(AlicePublicKey)).IsTrue();
        await Assert.That(secretKey.AsSpan().SequenceEqual(AliceSecretKey)).IsTrue();
    }

    [Test]
    public async Task ComputeClientSessionKeys()
    {
        var receiveKey = new byte[KeyExchange.SessionKeyBytes];
        var transmitKey = new byte[KeyExchange.SessionKeyBytes];
        KeyExchange.ComputeClientSessionKeys(
            receiveKey, transmitKey,
            AlicePublicKey, AliceSecretKey, BobPublicKey
        );

        await Assert.That(receiveKey.AsSpan().SequenceEqual(SessionKeyBobToAlice)).IsTrue();
        await Assert.That(transmitKey.AsSpan().SequenceEqual(SessionKeyAliceToBob)).IsTrue();
    }

    [Test]
    public async Task ComputeServerSessionKeys()
    {
        var receiveKey = new byte[KeyExchange.SessionKeyBytes];
        var transmitKey = new byte[KeyExchange.SessionKeyBytes];
        KeyExchange.ComputeServerSessionKeys(
            receiveKey, transmitKey,
            BobPublicKey, BobSecretKey, AlicePublicKey
        );

        await Assert.That(receiveKey.AsSpan().SequenceEqual(SessionKeyAliceToBob)).IsTrue();
        await Assert.That(transmitKey.AsSpan().SequenceEqual(SessionKeyBobToAlice)).IsTrue();
    }
}
