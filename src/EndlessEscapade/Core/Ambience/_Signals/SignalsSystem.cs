using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace EndlessEscapade.Core.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class SignalsSystem : ModSystem
{
    private sealed class SignalData(SignalUpdaterCallback callback)
    {
        public bool Enabled { get; set; }
        public readonly SignalUpdaterCallback Callback = callback;
    }

    public delegate bool SignalUpdaterCallback(in AmbienceContext context);

    private static readonly Dictionary<string, SignalData> Data = [];

    public override void Load()
    {
        base.Load();

        LoadModdedUpdaters(Mod);
        LoadVanillaUpdaters();
    }

    public override void PostUpdatePlayers()
    {
        base.PostUpdatePlayers();

        foreach (var (_, data) in Data)
        {
            data.Enabled = data.Callback?.Invoke(AmbienceContext.Default) ?? false;
        }
    }

    /// <summary>
    ///     Checks if a signal is active.
    /// </summary>
    /// <param name="name">The name of the signal to check.</param>
    /// <returns><c>true</c> if the signal was found and is active; otherwise, <c>false</c>.</returns>
    public static bool GetSignal(string name)
    {
        return Data[name].Enabled;
    }

    /// <summary>
    ///     Checks if all of the specified signals are active.
    /// </summary>
    /// <param name="names">The names of signals to check.</param>
    /// <returns><c>true</c> if all of the specified signals are active; otherwise, <c>false</c>.</returns>
    public static bool GetSignal(params string[] names)
    {
        var success = true;

        for (var i = 0; i < names.Length; i++)
        {
            if (!GetSignal(names[i]))
            {
                success = false;
                break;
            }
        }

        return success;
    }

    /// <summary>
    ///     Registers a new signal updater.
    /// </summary>
    /// <param name="name">The name of the signal to register.</param>
    /// <param name="callback">The callback of the signal to register.</param>
    public static void RegisterUpdater(string name, SignalUpdaterCallback callback)
    {
        Data[name] = new SignalData(callback);
    }

    private static void LoadModdedUpdaters(Mod mod)
    {
        foreach (var type in AssemblyManager.GetLoadableTypes(mod.Code))
        {
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                var attribute = method.GetCustomAttribute<SignalUpdaterAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                var callback = method.CreateDelegate<SignalUpdaterCallback>();
                var name = attribute.Name ?? method.Name;

                RegisterUpdater(name, callback);
            }
        }
    }

    private static void LoadVanillaUpdaters()
    {
        RegisterUpdater("Forest", static (in AmbienceContext context) => context.Player.ZonePurity);
        RegisterUpdater("Day", static (in AmbienceContext _) => Main.dayTime);
        RegisterUpdater("Night", static (in AmbienceContext _) => !Main.dayTime);
    }
}