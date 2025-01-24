namespace EndlessEscapade.Core.ECS;

[AttributeUsage(AttributeTargets.Method)]
public sealed class CalledInAttribute<T> : Attribute where T : Delegate;