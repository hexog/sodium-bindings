namespace SodiumBindings;

public static class SodiumHelper
{
    public static void Initialize()
    {
        sodium_init().EnsureSuccess();
    }
}
