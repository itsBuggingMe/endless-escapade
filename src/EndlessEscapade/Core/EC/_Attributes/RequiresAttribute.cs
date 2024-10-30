namespace EndlessEscapade.Core.EC;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RequiresAttribute(Type type) : Attribute
{
    public readonly Type Type = type;
}
