namespace EndlessEscapade.Core.Assets;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class GeneratedAssetExcludeAttribute(params string[] blacklist) : Attribute
{
    public string[] Blacklist { get; } = blacklist;
}