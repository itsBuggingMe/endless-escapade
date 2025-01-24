namespace EndlessEscapade.Framework.Core;

public abstract class ModCompositeTile : ModTile
{
    private const float TILE_FRAME_SIZE = 16f;
    private const float TILE_FRAME_PADDING = 2f;

    /// <summary>
    ///     Gets the width of each chunk, in pixels.
    /// </summary>
    public virtual int ChunkWidth { get; } = 72;

    /// <summary>
    ///     Gets the height of each chunk, in pixels.
    /// </summary>
    public virtual int ChunkHeight { get; } = 90;

    /// <summary>
    ///     Gets the number of horizontal chunks.
    /// </summary>
    public abstract int HorizontalChunkCount { get; }
    
    /// <summary>
    ///     Gets the number of vertical chunks.
    /// </summary>
    public abstract int VerticalChunkCount { get; }

    public sealed override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        var xOffset = i % HorizontalChunkCount * ChunkWidth;
        var yOffset = j % VerticalChunkCount * ChunkHeight;

        var tile = Framing.GetTileSafely(i, j);

        var tileAbove = Framing.GetTileSafely(i, j - 1);
        var tileBelow = Framing.GetTileSafely(i, j + 1);

        var tileLeft = Framing.GetTileSafely(i - 1, j);
        var tileRight = Framing.GetTileSafely(i + 1, j);

        var tileTopLeft = Framing.GetTileSafely(i - 1, j - 1);
        var tileTopRight = Framing.GetTileSafely(i + 1, j - 1);

        var tileBottomLeft = Framing.GetTileSafely(i - 1, j + 1);
        var tileBottomRight = Framing.GetTileSafely(i + 1, j + 1);

        var newFrameX = 0;
        var newFrameY = 0;
        
        if (tileAbove.HasTile && tileBelow.HasTile && tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 1;
            newFrameY = 1;

            if (tileTopRight.HasTile && tileBottomRight.HasTile)
            {
                newFrameX = 1;
                newFrameY = 4;
            }

            if (tileTopLeft.HasTile && tileBottomLeft.HasTile)
            {
                newFrameX = 2;
                newFrameY = 4;
            }

            if (tileTopLeft.HasTile && tileTopRight.HasTile)
            {
                newFrameX = 3;
                newFrameY = 2;
            }

            if (tileBottomLeft.HasTile && tileBottomRight.HasTile)
            {
                newFrameX = 3;
                newFrameY = 1;
            }
        }

        if (tileAbove.HasTile && tileBelow.HasTile && !tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 1;
            newFrameY = 3;
        }

        if (!tileAbove.HasTile && !tileBelow.HasTile && tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 0;
            newFrameY = 3;
        }

        if (!tileAbove.HasTile && !tileBelow.HasTile && !tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 2;
            newFrameY = 3;
        }

        if (!tileAbove.HasTile && tileBelow.HasTile && tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 1;
            newFrameY = 0;
        }

        if (tileAbove.HasTile && tileBelow.HasTile && !tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 0;
            newFrameY = 1;
        }

        if (tileAbove.HasTile && !tileBelow.HasTile && tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 1;
            newFrameY = 2;
        }

        if (tileAbove.HasTile && tileBelow.HasTile && tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 2;
            newFrameY = 1;
        }

        if (!tileAbove.HasTile && tileBelow.HasTile && !tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 3;
            newFrameY = 0;
        }

        if (tileAbove.HasTile && !tileBelow.HasTile && !tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 3;
            newFrameY = 3;
        }

        if (!tileAbove.HasTile && !tileBelow.HasTile && !tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 0;
            newFrameY = 4;
        }

        if (!tileAbove.HasTile && !tileBelow.HasTile && tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 3;
            newFrameY = 4;
        }

        if (!tileAbove.HasTile && tileBelow.HasTile && !tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 0;
            newFrameY = 0;
        }

        if (!tileAbove.HasTile && tileBelow.HasTile && tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 2;
            newFrameY = 0;
        }

        if (tileAbove.HasTile && !tileBelow.HasTile && !tileLeft.HasTile && tileRight.HasTile)
        {
            newFrameX = 0;
            newFrameY = 2;
        }

        if (tileAbove.HasTile && !tileBelow.HasTile && tileLeft.HasTile && !tileRight.HasTile)
        {
            newFrameX = 2;
            newFrameY = 2;
        }

        var tileScale = TILE_FRAME_SIZE + TILE_FRAME_PADDING;

        tile.TileFrameX = (short)(newFrameX * tileScale + xOffset);
        tile.TileFrameY = (short)(newFrameY * tileScale + yOffset);

        return false;
    }
}