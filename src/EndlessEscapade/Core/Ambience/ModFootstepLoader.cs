using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EndlessEscapade.Common.Tiles;
using EndlessEscapade.Core.Configuration;
using EndlessEscapade.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class ModFootstepLoader : ModSystem
{
    private sealed class FootstepPlayer : ModPlayer
    {
        private enum LegType
        {
            Left,
            Right
        }

        private LegType type;

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (!ClientConfiguration.Instance.EnableFootsteps)
            {
                return;
            }

            UpdateLegs();
            UpdateImpact();
            UpdateFootsteps();
        }

        private void UpdateLegs()
        {
            var frame = Player.legFrame.Y / Player.legFrame.Height;

            if (frame != 0 || Player.IsGrounded())
            {
                return;
            }

            type = LegType.Left;
        }

        private void UpdateImpact()
        {
            var isGrounded = Player.IsGrounded();
            var wasGrounded = Player.WasGrounded();

            var justLanded = isGrounded && !wasGrounded;
            var justJumped = !isGrounded && wasGrounded;

            if (!justLanded && !justJumped)
            {
                return;
            }

            var position = justLanded ? Player.Bottom : Player.oldPosition + new Vector2(Player.width / 2f, Player.height);
            var tile = Framing.GetTileSafely(position);

            if (!tile.HasTile)
            {
                return;
            }

            PlayTileFootstep(tile);
        }

        private void UpdateFootsteps()
        {
            if (!Player.IsGrounded())
            {
                return;
            }

            var frame = Player.legFrame.Y / Player.legFrame.Height;
            var isWalking = (type == LegType.Left && (frame == 16 || frame == 17)) || (type == LegType.Right && (frame == 9 || frame == 10));

            if (!isWalking)
            {
                return;
            }

            var tile = Framing.GetTileSafely(Player.Bottom);

            if (!tile.HasTile)
            {
                return;
            }

            type = type == LegType.Left ? LegType.Right : LegType.Left;

            PlayTileFootstep(tile);
        }

        private void PlayTileFootstep(Tile tile)
        {
            var hasFootstep = TryGetFootstep(tile.TileType, out var footstep);

            if (!hasFootstep)
            {
                return;
            }

            SoundEngine.PlaySound(footstep.Sound, Player.Bottom);
        }
    }

    private static readonly Dictionary<string, ModFootstep> Footsteps = [];

    public override void PostSetupContent()
    {
        base.PostSetupContent();

        LoadAssociations();
    }

    public static bool TryGetFootstep(int tileType, [MaybeNullWhen(false)] out ModFootstep? footstep)
    {
        footstep = null;

        if (!TileMaterialSystem.TryGetMaterial(tileType, out var materialName))
        {
            return false;
        }

        return Footsteps.TryGetValue(materialName, out footstep);
    }

    private static void LoadAssociations()
    {
        foreach (var footstep in ModContent.GetContent<ModFootstep>())
        {
            Footsteps[footstep.Material] = footstep;
        }
    }
}