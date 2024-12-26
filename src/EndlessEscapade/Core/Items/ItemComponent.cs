namespace EndlessEscapade.Core.Items;

public abstract class ItemComponent : GlobalItem
{
    /// <summary>
    ///     Whether this component is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }
    
    public sealed override bool InstancePerEntity { get; } = true;
}