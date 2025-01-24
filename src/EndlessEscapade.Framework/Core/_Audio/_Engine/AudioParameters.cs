namespace EndlessEscapade.Framework.Core;

public struct AudioParameters
{
    private float lowPass;

    public float LowPass
    {
        get => lowPass;
        set => lowPass = MathHelper.Clamp(value, 0f, 1f);
    }
}