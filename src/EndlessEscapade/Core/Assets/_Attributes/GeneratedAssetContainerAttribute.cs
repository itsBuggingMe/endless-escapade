namespace EndlessEscapade.Core.Assets;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GeneratedAssetContainerAttribute<T>(string path) : Attribute where T : class
{
    public string Path { get; } = path;
    
    public Type Type { get; } = typeof(T);
}