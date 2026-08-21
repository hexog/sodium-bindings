# SodiumBindings

SodiumBindings provides .NET bindings for the
[libsodium](https://doc.libsodium.org/) cryptography library. The API uses
`Span<T>` and `ReadOnlySpan<T>` so callers can control allocations and handle
sensitive buffers directly.

## Requirements

- .NET 10 or later
- A runtime supported by the [libsodium NuGet dependency](https://www.nuget.org/packages/libsodium)

## Installation

```shell
dotnet add package SodiumBindings
```

## Getting started

Initialize libsodium once during application startup before using any other
SodiumBindings API:

```csharp
using SodiumBindings;

SodiumHelper.Initialize();
```

The following example encrypts and decrypts a message with `SecretBox`:

```csharp
using System.Security.Cryptography;
using System.Text;
using SodiumBindings;

SodiumHelper.Initialize();

byte[] key = RandomNumberGenerator.GetBytes(SecretBox.KeyBytes);
byte[] nonce = RandomNumberGenerator.GetBytes(SecretBox.NonceBytes);
byte[] message = Encoding.UTF8.GetBytes("Hello, libsodium!");
byte[] ciphertext = new byte[SecretBox.GetCiphertextLength(message.Length)];

SecretBox.Encrypt(ciphertext, message, nonce, key);

byte[] plaintext = new byte[SecretBox.GetPlaintextLength(ciphertext.Length)];
if (!SecretBox.Decrypt(plaintext, ciphertext, nonce, key))
{
    throw new CryptographicException("The ciphertext failed authentication.");
}

Console.WriteLine(Encoding.UTF8.GetString(plaintext));

CryptographicOperations.ZeroMemory(key);
CryptographicOperations.ZeroMemory(plaintext);
```

## Security notes

- Consult the [libsodium documentation](https://doc.libsodium.org/) before
  choosing algorithms and parameters for a production protocol.
- Allocate output buffers using the size constants and length helpers exposed
  by each API.
- Never reuse a nonce with the same key unless the underlying libsodium
  construction explicitly permits it.

## License

SodiumBindings is licensed under the
[MIT License](https://github.com/hexog/sodium-bindings/blob/master/LICENSE).
