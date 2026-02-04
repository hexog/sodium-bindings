namespace SodiumBindings.Tests;

public static class NativeExtensions
{
    extension(int returnValue)
    {
        public async Task EnsureSuccess()
        {
            await Assert.That(returnValue).IsEqualTo(0);
        }
    }
}
