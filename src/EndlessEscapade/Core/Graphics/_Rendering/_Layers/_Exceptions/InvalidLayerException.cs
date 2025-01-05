namespace EndlessEscapade.Core.Graphics;

public sealed class InvalidLayerException : Exception
{
    public InvalidLayerException() { }

    public InvalidLayerException(string message) : base(message) { }

    public InvalidLayerException(string message, Exception? innerException) : base(message, innerException) { }
}