using EndlessEscapade.Core.ECS;

namespace EndlessEscapade.Tests;

public sealed class EntitySystemTests
{
    [Test]
    public void Create_CreatesEntities()
    {
        EntitySystem.Create();

        Assert.IsTrue(EntitySystem.Has(0));
    }

    [Test]
    public void Destroy_DestroysEntities()
    {
        EntitySystem.Create();

        Assert.IsTrue(EntitySystem.Destroy(0));
        
        Assert.IsFalse(EntitySystem.Has(0));
    }

    [Test]
    public void Has_ChecksEntities()
    {
        EntitySystem.Create();
        
        Assert.IsTrue(EntitySystem.Has(1));
    }
}