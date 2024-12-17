using EndlessEscapade.Core.Audio;
using EndlessEscapade.Utilities;
using Terraria.Audio;

namespace EndlessEscapade.Core.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class WaterMufflingPlayer : ModPlayer
{
    public static readonly SoundStyle WaterSplashSound = new($"{nameof(EndlessEscapade)}/Assets/Sounds/Ambience/Sounds/Water/WaterSplash")
    {
        PitchVariance = 0.2f
    };

    private float _intensity;

    public float Intensity
    {
        get => _intensity;
        set => _intensity = MathHelper.Clamp(value, 0f, 0.9f);
    }

    public override void PostUpdate()
    {
        base.PostUpdate();

        UpdateSplash();
        UpdateMuffling();
    }

    private void UpdateSplash()
    {
        // The game sets Player.wetCount to 10 whenever the player exits/enters water.
        // We check for 5 to make the splash play midway through.
        if (Player.wetCount != 5)
        {
            return;
        }

        SoundEngine.PlaySound(in WaterSplashSound, Player.Center);
    }

    private void UpdateMuffling()
    {
        if (Player.IsUnderwater())
        {
            Intensity += 0.05f;
        }
        else
        {
            Intensity -= 0.05f;
        }

        if (Intensity <= 0f)
        {
            return;
        }

        AudioSystem.AddModifier
        (
            $"{nameof(EndlessEscapade)}:{nameof(WaterMufflingPlayer)}",
            60,
            (ref AudioParameters parameters, float progress) => parameters.LowPass = Intensity * progress
        );
    }
}