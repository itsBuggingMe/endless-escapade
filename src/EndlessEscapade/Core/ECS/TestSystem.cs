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

        foreach (var entity in new EntityQuery<Left, Right>())
        {
            ref var left = ref entity.Get<Left>();
            ref var right = ref entity.Get<Right>();

            left.Value += right.Value;
        }
    }

    private Entity entity;
    
    public override void PostUpdateInput()
    {
        base.PostUpdateInput();

        if (Main.keyState.IsKeyDown(Keys.F) && !Main.oldKeyState.IsKeyDown(Keys.F))
        {
            entity = EntitySystem.Create()
                .Set(new Left(0))
                .Set(new Right(1));
        }
        
    }
}