namespace EndlessEscapade.Common.World;

public abstract class ModChestLoot : ModType
{
    public struct StackData
    {
        private readonly int minStack;
        private readonly int maxStack;

        public int Value => WorldGen.genRand.Next(minStack, maxStack);

        public StackData(int stack) {
            minStack = stack;
            maxStack = stack;
        }

        public StackData(int minStack, int maxStack) {
            this.minStack = minStack;
            this.maxStack = maxStack;
        }
    }

    public int TileType {
        get {
            var split = TilePath.Split('/');

            var prefix = split[0];
            var suffix = split[1];

            return prefix == "Terraria" ? TileID.Search.GetId(suffix) : ModContent.Find<ModTile>(TilePath).Type;
        }
    }

    public int ItemType {
        get {
            var split = ItemPath.Split('/');

            var prefix = split[0];
            var suffix = split[1];

            return prefix == "Terraria" ? ItemID.Search.GetId(suffix) : ModContent.Find<ModItem>(ItemPath).Type;
        }
    }

    protected abstract string TilePath { get; }

    protected abstract string ItemPath { get; }

    public abstract int[] Frames { get; }

    public virtual StackData Stack { get; } = new(1);

    public virtual int Chance { get; }

    public virtual bool RandomSlot { get; }

    protected sealed override void Register() {
        ModTypeLookup<ModChestLoot>.Register(this);
    }
}
