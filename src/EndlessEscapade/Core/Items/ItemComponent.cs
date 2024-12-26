namespace EndlessEscapade.Core.Items;

public abstract class ItemComponent : GlobalItem
{
    public sealed override bool InstancePerEntity { get; } = true;

    public bool Enabled { get; set; }
}