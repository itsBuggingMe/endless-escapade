namespace EndlessEscapade.Core.ECS.Systems;

internal interface ISystem
{
    /// <summary>
    /// Lower == executed earlier
    /// </summary>
    int Order => 0;
    Query BuildQuery(World world);
    public void Execute(Query query);
}
