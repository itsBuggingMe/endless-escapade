namespace EndlessEscapade.Core.EC;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class AfterAttribute(Type type) : Attribute
{
    public readonly Type Type = type;
}
