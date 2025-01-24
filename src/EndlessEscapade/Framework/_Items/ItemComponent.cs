namespace EndlessEscapade.Framework;

/// <summary>
///     Provides a base class for defining custom components that extend the behavior of items.
/// </summary>
public abstract class ItemComponent : GlobalItem
{
    /// <summary>
    ///     Gets or sets whether this component is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }
    
    public sealed override bool InstancePerEntity { get; } = true;
}