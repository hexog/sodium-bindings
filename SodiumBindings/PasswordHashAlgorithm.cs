using System.Diagnostics.CodeAnalysis;

namespace SodiumBindings;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum PasswordHashAlgorithm
{
    Argon2i13 = 1,
    Argon2id13 = 2
}
