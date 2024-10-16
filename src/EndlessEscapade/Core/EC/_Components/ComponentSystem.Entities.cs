namespace EndlessEscapade.Core.EC;

public sealed partial class ComponentSystem : ModSystem
{
	private static partial class ComponentData<T> where T : struct
	{
		public static readonly int Id = componentTypeCount++;
		public static readonly int Mask = 1 << Id;

		public static long[] Flags = [];

		public static T[] Components = [];
	}

	private static int componentTypeCount;

	public static ref T Get<T>(int id) where T : struct {
		return ref ComponentData<T>.Components[id];
	}

	public static ref T Set<T>(int id, T value) where T : struct {
		if (id >= ComponentData<T>.Components.Length) {
			var newSize = Math.Max(1, ComponentData<T>.Components.Length);

			while (newSize <= id) {
				newSize *= 2;
			}

			Array.Resize(ref ComponentData<T>.Components, newSize);
		}

		if (id >= ComponentData<T>.Flags.Length) {
			var newSize = Math.Max(1, ComponentData<T>.Flags.Length);

			while (newSize <= id) {
				newSize *= 2;
			}

			Array.Resize(ref ComponentData<T>.Flags, newSize);
		}

		ComponentData<T>.Components[id] = value;
		ComponentData<T>.Flags[id] |= ComponentData<T>.Mask;

		return ref ComponentData<T>.Components[id];
	}

	public static bool Has<T>(int id) where T : struct {
		return (ComponentData<T>.Flags[id] & ComponentData<T>.Mask) != 0;
	}

	public static bool Remove<T>(int id) where T : struct {
		ComponentData<T>.Flags[id] &= ~ComponentData<T>.Mask;

		return true;
	}
}
