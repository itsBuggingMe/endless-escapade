using System;
using Newtonsoft.Json;

namespace EndlessEscapade.Generators.Data;

public sealed class AmbienceTrackData : IEquatable<AmbienceTrackData>
{
    [JsonRequired]
    public SoundStyleData SoundStyleData;

    [JsonRequired]
    public string[] Signals;

    public float StepIn = 0.01f;

    public float StepOut = 0.01f;

    public bool Equals(AmbienceTrackData other) {
        return other != null
            && other.SoundStyleData == SoundStyleData
            && other.Signals.AsSpan().SequenceEqual(other.Signals)
            && other.StepIn == StepIn
            && other.StepOut == StepOut;
    }

    public override bool Equals(object obj) {
        return obj is AmbienceTrackData data && Equals(data);
    }

    public override int GetHashCode() {
        return HashCode.Combine(
            Signals,
            SoundStyleData,
            StepIn,
            StepOut
        );
    }
}
