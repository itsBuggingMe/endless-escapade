namespace EndlessEscapade.Common.Tiles;

[AttributeUsage(AttributeTargets.Class)]
public sealed class TileMaterialAttribute(string name) : Attribute
{
	/// <summary>
	///		The name of the material associated with this attribute's type.
	/// </summary>
    public readonly string Name = name;
}
