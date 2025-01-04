namespace EndlessEscapade.Core.ECS;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class CalledAfterAttribute<T> : Attribute where T : class;
