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

    public static long OperationsLimitMin { get; } = (long)crypto_pwhash_opslimit_min();

    public static long OperationsLimitInteractive { get; } = (long)crypto_pwhash_opslimit_interactive();

    public static long OperationsLimitModerate { get; } = (long)crypto_pwhash_opslimit_moderate();

    public static long OperationsLimitSensitive { get; } = (long)crypto_pwhash_opslimit_sensitive();

    public static long OperationsLimitMax { get; } = (long)crypto_pwhash_opslimit_max();

    public static long MemoryLimitMin { get; } = (long)crypto_pwhash_memlimit_min();

    public static long MemoryLimitInteractive { get; } = (long)crypto_pwhash_memlimit_interactive();

    public static long MemoryLimitModerate { get; } = (long)crypto_pwhash_memlimit_moderate();

    public static long MemoryLimitSensitive { get; } = (long)crypto_pwhash_memlimit_sensitive();

    public static long MemoryLimitMax { get; } = (long)crypto_pwhash_memlimit_max();

    public static int SaltBytes { get; } = (int)crypto_pwhash_saltbytes();

    public static int StringBytes { get; } = (int)crypto_pwhash_strbytes();

    public static int PasswordMin { get; } = (int)crypto_pwhash_passwd_min();

    public static int BytesMin { get; } = (int)crypto_pwhash_bytes_min();

    public static void Hash(
        Span<byte> output,
        ReadOnlySpan<byte> password,
        ReadOnlySpan<byte> salt,
        long operationsLimit,
        long memoryLimit,
        PasswordHashAlgorithm algorithm
    )
    {
        Validate.GreaterOrEqualTo(output.Length, BytesMin);
        Validate.GreaterOrEqualTo(password.Length, PasswordMin);
        Validate.GreaterOrEqualTo(salt.Length, SaltBytes);
        Validate.Range(operationsLimit, OperationsLimitMin, OperationsLimitMax);
        Validate.Range(memoryLimit, OperationsLimitMin, MemoryLimitMax);

        var outputLen = (nuint)output.Length;
        var passwordLen = (nuint)password.Length;
        var opsLimit = (ulong)operationsLimit;
        var memLimit = (nuint)memoryLimit;
        var alg = (int)algorithm;
        crypto_pwhash(output, outputLen, password, passwordLen, salt, opsLimit, memLimit, alg).EnsureSuccess();
    }

    public static void HashToString(Span<byte> output, ReadOnlySpan<byte> password, long operationsLimit, long memoryLimit)
    {
        Validate.GreaterOrEqualTo(output.Length, StringBytes);
        Validate.GreaterOrEqualTo(password.Length, PasswordMin);
        Validate.Range(operationsLimit, OperationsLimitMin, OperationsLimitMax);
        Validate.Range(memoryLimit, OperationsLimitMin, MemoryLimitMax);

        var passwordLen = (nuint)password.Length;
        var opsLimit = (ulong)operationsLimit;
        var memLimit = (nuint)memoryLimit;
        crypto_pwhash_str(output, password, passwordLen, opsLimit, memLimit).EnsureSuccess();
    }

    public static bool VerifyString(
        ReadOnlySpan<byte> str,
        ReadOnlySpan<byte> password
    )
    {
        Validate.GreaterOrEqualTo(str.Length, StringBytes);
        Validate.GreaterOrEqualTo(password.Length, PasswordMin);

        var exitCode = crypto_pwhash_str_verify(str, password, (nuint)password.Length);
        return exitCode == 0;
    }
}
