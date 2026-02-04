namespace SodiumBindings;

public class SodiumException(string message, Exception? innerException = null) : Exception(message, innerException);
