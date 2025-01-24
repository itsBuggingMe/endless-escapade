namespace EndlessEscapade.Utilities;

public static class RenderUtils
{
    /// <summary>
    ///     Gets the rectangle that represents the screen bounds.
    /// </summary>
    public static Rectangle ScreenBounds => new(0, 0, Main.screenWidth, Main.screenHeight);
}