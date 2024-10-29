namespace EndlessEscapade.Core.EC;

[Autoload(false)]
public abstract partial class ProjectileComponent : GlobalProjectile
{
    public sealed override bool InstancePerEntity { get; } = true;

    /// <summary>
    ///     Whether this component is enabled or not.
    /// </summary>
    public bool Enabled {
        get => enabled;
        set => enabled = value;
    }

    private bool enabled;
}
