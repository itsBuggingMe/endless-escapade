namespace EndlessEscapade.Core.Graphics;

public sealed class InvalidRenderLayerException : Exception
{
    public InvalidRenderLayerException() { }

    public InvalidRenderLayerException(string message) : base(message) { }

    public InvalidRenderLayerException(string message, Exception? innerException) : base(message, innerException) { }
}