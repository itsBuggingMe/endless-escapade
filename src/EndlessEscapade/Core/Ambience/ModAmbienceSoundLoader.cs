using EndlessEscapade.Core.Configuration;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class ModAmbienceSoundLoader : ModSystem
{
    public override void PostUpdateWorld()
    {
        base.PostUpdateWorld();

        UpdateSounds();
    }

    private static void UpdateSounds()
    {
        if (!ClientConfiguration.Instance.EnableAmbienceSounds)
        {
            return;
        }

        foreach (var sound in ModContent.GetContent<ModAmbienceSound>())
        {
            var active = sound.IsAmbienceActive(AmbienceContext.Default);

            if (!active || !Main.rand.NextBool(sound.Chance) || SoundEngine.TryGetActiveSound(sound.Slot, out _))
            {
                continue;
            }

            sound.Slot = SoundEngine.PlaySound(sound.Sound);
        }
    }
}