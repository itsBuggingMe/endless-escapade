namespace EndlessEscapade.Core.Assets;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GeneratedSoundContainerAttribute(string path) : Attribute
{
    public string Path { get; } = path;
}