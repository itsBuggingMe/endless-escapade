using Terraria.DataStructures;

namespace EndlessEscapade.Core.EC;

public sealed class Enabler : GlobalProjectile
{
    public override bool InstancePerEntity { get; } = true;

    public override void SetDefaults(Projectile entity) {
        base.SetDefaults(entity);

        entity.TryEnableComponent<AComponent>();
    }
}

[Requires(typeof(BComponent))]
[After(typeof(BComponent))]
public sealed class AComponent : ProjectileComponent
{
}

public sealed class BComponent : ProjectileComponent
{

}