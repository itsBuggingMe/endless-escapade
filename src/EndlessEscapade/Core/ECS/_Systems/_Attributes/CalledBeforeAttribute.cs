namespace EndlessEscapade.Core.ECS;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class CalledBeforeAttribute<T> : Attribute where T : class;