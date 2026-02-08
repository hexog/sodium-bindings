using System.Diagnostics;

namespace SodiumBindings;

public static class PasswordHash
{
    public static PasswordHashAlgorithm DefaultAlgorithm
    {
        get
        {
            var value = (PasswordHashAlgorithm)crypto_pwhash_alg_default();
            Debug.Assert(Enum.IsDefined(value));
            return value;
        }
    }

    public static ulong OperationsLimitInteractive => crypto_pwhash_opslimit_interactive();

    public static ulong OperationsLimitModerate => crypto_pwhash_opslimit_moderate();

    public static ulong OperationsLimitSensitive => crypto_pwhash_opslimit_sensitive();

    public static ulong MemoryLimitInteractive => crypto_pwhash_memlimit_interactive();

    public static ulong MemoryLimitModerate => crypto_pwhash_memlimit_moderate();

    public static ulong MemoryLimitSensitive => crypto_pwhash_memlimit_sensitive();

    public static ulong SaltBytes => crypto_pwhash_saltbytes();

    public static ulong StringBytes => crypto_pwhash_strbytes();

    public static void Hash(
        Span<byte> output,
        ReadOnlySpan<byte> password,
        ReadOnlySpan<byte> salt,
        ulong operationsLimit,
        ulong memoryLimit,
        PasswordHashAlgorithm algorithm
    )
    {
        Validate.Range(output.Length, crypto_pwhash_bytes_min(), crypto_pwhash_bytes_max());
        Validate.Range(password.Length, crypto_pwhash_passwd_min(), crypto_pwhash_passwd_max());
        Validate.GreaterOrEqualTo(salt.Length, crypto_pwhash_saltbytes());
        Validate.Range(operationsLimit, crypto_pwhash_opslimit_min(), crypto_pwhash_opslimit_max());
        Validate.Range(memoryLimit, crypto_pwhash_memlimit_min(), crypto_pwhash_memlimit_max());

        crypto_pwhash(output, (nuint)output.Length, password, (nuint)password.Length, salt, operationsLimit, (nuint)memoryLimit, (int)algorithm).EnsureSuccess();
    }

    public static void HashToString(Span<byte> output, ReadOnlySpan<byte> password, ulong operationsLimit, ulong memoryLimit)
    {
        Validate.GreaterOrEqualTo(output.Length, crypto_pwhash_strbytes());
        Validate.Range(password.Length, crypto_pwhash_passwd_min(), crypto_pwhash_passwd_max());
        Validate.Range(operationsLimit, crypto_pwhash_opslimit_min(), crypto_pwhash_opslimit_max());
        Validate.Range(memoryLimit, crypto_pwhash_memlimit_min(), crypto_pwhash_memlimit_max());

        crypto_pwhash_str(output, password, (nuint)password.Length, operationsLimit, (nuint)memoryLimit).EnsureSuccess();
    }

    public static bool VerifyString(
        ReadOnlySpan<byte> str,
        ReadOnlySpan<byte> password
    )
    {
        Validate.GreaterOrEqualTo(str.Length, crypto_pwhash_strbytes());
        Validate.Range(password.Length, crypto_pwhash_passwd_min(), crypto_pwhash_passwd_max());

        var exitCode = crypto_pwhash_str_verify(str, password, (nuint)password.Length);
        return exitCode == 0;
    }
}
