namespace EndlessEscapade.Core.EC;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class RequiredAttribute(Type type) : Attribute
{
    public readonly Type Type = type;
}
