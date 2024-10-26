using Terraria.DataStructures;

namespace EndlessEscapade.Core.EC;

[After(typeof(BComponent))]
public sealed class AComponent : ProjectileComponent
{
    public override void OnSpawn(Projectile projectile, IEntitySource source) {
        base.OnSpawn(projectile, source);

        Main.NewText("Run component A");
    }
}

public sealed class BComponent : ProjectileComponent
{
    public override void OnSpawn(Projectile projectile, IEntitySource source) {
        base.OnSpawn(projectile, source);

        Main.NewText("Run component B");
    }
}

[After(typeof(AComponent))]
public sealed class CComponent : ProjectileComponent
{
    public override void OnSpawn(Projectile projectile, IEntitySource source) {
        base.OnSpawn(projectile, source);

        Main.NewText("Run component C");
    }
}
