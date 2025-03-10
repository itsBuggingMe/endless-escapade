namespace EndlessEscapade.Core.ECS;

public struct Entity(int entityID, int entityVersion, World world)
{
    private int EntityID = entityID;
    private int EntityVersion = entityVersion;
    private World World = world;
}
