namespace EndlessEscapade.Core.World;

public abstract class ModChestLoot : ModType
{
    /// <summary>
    ///     The type of the item associated with this loot. Defaults to <see cref="TileID.Containers" />.
    /// </summary>
    public abstract int ItemType { get; }

    /// <summary>
    ///     The frames of the tile associated with this loot. Defaults to <c>0</c>.
    /// </summary>
    public abstract int[] Frames { get; }

    /// <summary>
    ///     Whether this loot's <see cref="Item" /> should be placed in a random empty slot or not. Defaults to <c>false</c>.
    /// </summary>
    public virtual bool RandomSlot { get; }

    /// <summary>
    ///     The chance denominator for this loot's <see cref="Item" /> to be generated. Defaults to <c>1</c>.
    /// </summary>
    public virtual int ChanceDenominator { get; } = 1;

    /// <summary>
    ///     The minimum stack size of the loot when generated. Defaults to <c>1</c>.
    /// </summary>
    public virtual int MinStack { get; } = 1;
    
    /// <summary>
    ///     The maximum stack size of the loot when generated. Defaults to <c>1</c>.
    /// </summary>
    public virtual int MaxStack { get; } = 1;

    /// <summary>
    ///     The stack size of the loot, randomly calculated between <see cref="MinStack" /> and <see cref="MaxStack" />.
    /// </summary>
    /// <remarks>
    ///     Set the value of <see cref="MinStack"/> to the same value of <see cref="MaxStack"/> if you don't want a randomized stack.
    /// </remarks>
    public int Stack => Main.rand.Next(MinStack, MaxStack);

    /// <summary>
    ///     The type of the tile associated with this loot. Defaults to <see cref="TileID.Containers" />.
    /// </summary>
    public virtual int TileType { get; } = TileID.Containers;

    protected sealed override void Register()
    {
        ModTypeLookup<ModChestLoot>.Register(this);
    }
}