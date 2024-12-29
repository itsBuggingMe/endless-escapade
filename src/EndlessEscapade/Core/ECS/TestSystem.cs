using Microsoft.Xna.Framework.Input;

namespace EndlessEscapade.Core.ECS;

public class TestSystem : ModSystem
{
    public struct Left(int value)
    {
        public int Value = value;
    }

    public struct Right(int value)
    {
        public int Value = value;
    }
    
    public override void PostUpdateWorld()
    {
        base.PostUpdateWorld();

        foreach (var entity in EntityQuery<Left, Right>.Enumerate())
        {
            ref var left = ref entity.Get<Left>();
            ref var right = ref entity.Get<Right>();

            left.Value += right.Value;
        }
    }

    public override void PostUpdateInput()
    {
        base.PostUpdateInput();

        if (Main.keyState.IsKeyDown(Keys.F) && !Main.oldKeyState.IsKeyDown(Keys.F))
        {
            if (EntitySystem.Has(0))
            {
                EntitySystem.Destroy(0);
            }
            else
            {
                EntitySystem.Create();
            }
        }

        foreach (var entity in EntitySystem.Enumerate())
        {
            if (EntitySystem.Has(entity))
            {
                Main.NewText(EntitySystem.Get(entity));
            }
        }
    }
}