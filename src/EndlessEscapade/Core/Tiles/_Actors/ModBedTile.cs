using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ObjectData;

namespace EndlessEscapade.Framework;

public abstract class ModBedTile<TModItem> : ModTile where TModItem : ModItem
{
    public const int NextStyleHeight = 38;

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        Main.tileLavaDeath[Type] = true;
        Main.tileFrameImportant[Type] = true;

        TileID.Sets.HasOutlines[Type] = true;
        TileID.Sets.CanBeSleptIn[Type] = true;
        TileID.Sets.IsValidSpawnPoint[Type] = true;
        TileID.Sets.DisableSmartCursor[Type] = true;
        TileID.Sets.InteractibleByNPCs[Type] = true;

        AddToArray(ref TileID.Sets.RoomNeeds.CountsAsChair);

        AdjTiles = new int[]
        {
            TileID.Beds
        };

        TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);

        TileObjectData.newTile.CoordinateHeights = new[]
        {
            16,
            18
        };

        TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, -2);

        TileObjectData.addTile(Type);
    }

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
    {
        return true;
    }

    public override void ModifySmartInteractCoords(ref int width, ref int height, ref int frameWidth, ref int frameHeight, ref int extraY)
    {
        base.ModifySmartInteractCoords(ref width, ref height, ref frameWidth, ref frameHeight, ref extraY);
        
        width = 2; 
        height = 2; 
    }

    public override void NumDust(int i, int j, bool fail, ref int num)
    {
        base.NumDust(i, j, fail, ref num);

        num = fail ? 1 : 3;
    }

    public override bool RightClick(int i, int j)
    {
        var tile = Main.tile[i, j];
        
        var spawnX = i - tile.TileFrameX / 18 + (tile.TileFrameX >= 72 ? 5 : 2);
        var spawnY = j + 2;

        if (tile.TileFrameY % NextStyleHeight != 0)
        {
            spawnY--;
        }

        var player = Main.LocalPlayer;
        
        if (!Player.IsHoveringOverABottomSideOfABed(i, j) && player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
        {
            player.GamepadEnableGrappleCooldown();
            
            player.sleeping.StartSleeping(player, i, j);
        }
        else
        {
            player.FindSpawn();

            if (player.SpawnX == spawnX && player.SpawnY == spawnY)
            {
                player.RemoveSpawn();
                
                Main.NewText(Language.GetTextValue("Game.SpawnPointRemoved"), byte.MaxValue, 240, 20);
            }
            else if (Player.CheckSpawn(spawnX, spawnY))
            {
                player.ChangeSpawn(spawnX, spawnY);
                
                Main.NewText(Language.GetTextValue("Game.SpawnPointSet"), byte.MaxValue, 240, 20);
            }
        }

        return true;
    }

    public override void MouseOver(int i, int j)
    {
        base.MouseOver(i, j);
        
        var player = Main.LocalPlayer;

        if (!Player.IsHoveringOverABottomSideOfABed(i, j) && player.IsWithinSnappngRangeToTile(i, j, PlayerSleepingHelper.BedSleepingMaxDistance))
        {
            player.noThrow = 2;
            
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ItemID.SleepingIcon;
        }
        else
        {
            player.noThrow = 2;
            
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<TModItem>();
        }
    }
}