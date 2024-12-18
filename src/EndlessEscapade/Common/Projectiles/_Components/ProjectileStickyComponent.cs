using System.IO;
using EndlessEscapade.Core.Projectiles;
using Terraria.ModLoader.IO;

namespace EndlessEscapade.Common.Projectiles;

public sealed class ProjectileStickyComponent : ProjectileComponent
{
    /// <summary>
    ///     Whether the <see cref="Projectile"/> attached to this component is sticking to an NPC or not.
    /// </summary>
    public bool StickingToNPC { get; private set; }
    
    /// <summary>
    ///     Whether the <see cref="Projectile"/> attached to this component is sticking to a tile or not.
    /// </summary>
    public bool StickingToTile { get; private set; }
    
    public override GlobalProjectile Clone(Projectile? from, Projectile to)
    {
        var clone = base.Clone(from, to);

        if (!Enabled || clone is not ProjectileStickyComponent component) {
            return clone;
        }

        component.StickingToTile = StickingToTile;
        component.StickingToNPC = StickingToNPC;

        return clone;
    }
    
    public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
        base.SendExtraAI(projectile, bitWriter, binaryWriter);

        if (!Enabled)
        {
            return;
        }
        
        binaryWriter.Write(StickingToNPC);
        binaryWriter.Write(StickingToTile);
    }

    public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
    {
        base.ReceiveExtraAI(projectile, bitReader, binaryReader);

        if (!Enabled)
        {
            return;
        }

        StickingToNPC = binaryReader.ReadBoolean();
        StickingToTile = binaryReader.ReadBoolean();
    }
}