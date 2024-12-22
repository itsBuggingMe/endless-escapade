using EndlessEscapade.Core.Configuration;
using ReLogic.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class ModAmbienceTrackLoader : ModSystem
{
    public override void PostUpdateWorld()
    {
        base.PostUpdateWorld();

        UpdateTracks();
    }

    private static void UpdateTracks()
    {
        if (!ClientConfiguration.Instance.EnableAmbienceTracks)
        {
            return;
        }

        foreach (var track in ModContent.GetContent<ModAmbienceTrack>())
        {
            var active = track.IsAmbienceActive(AmbienceContext.Default);

            if (active)
            {
                track.Volume += track.StepIn;
            }
            else
            {
                track.Volume -= track.StepOut;
            }

            var trackPlaying = SoundEngine.TryGetActiveSound(track.Slot, out var instance) && instance?.IsPlaying == true;

            if (active)
            {
                if (trackPlaying)
                {
                    instance.Volume = track.Volume;
                }
                else
                {
                    track.Slot = SoundEngine.PlaySound(track.Sound);
                    track.Volume = 0f;

                    if (!SoundEngine.TryGetActiveSound(track.Slot, out instance))
                    {
                        return;
                    }

                    instance.Volume = 0f;
                }
            }
            else if (trackPlaying)
            {
                if (track.Volume > 0f)
                {
                    instance.Volume = track.Volume;
                }
                else
                {
                    instance.Stop();
                    track.Slot = SlotId.Invalid;
                }
            }
        }
    }
}