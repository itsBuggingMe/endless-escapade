using System;
using Newtonsoft.Json;

namespace EndlessEscapade.Generators.Data;

public sealed class AmbienceSoundData : IEquatable<AmbienceSoundData>
{
    [JsonRequired]
    public SoundStyleData SoundStyleData;

    [JsonRequired]
    public int Chance;

    [JsonRequired]
    public string[] Signals;

    public bool Equals(AmbienceSoundData other) {
        return other.Chance == Chance
            && other.SoundStyleData == SoundStyleData
            && other.Signals.AsSpan().SequenceEqual(Signals);
    }

    public override bool Equals(object obj) {
        return obj is AmbienceSoundData data && Equals(data);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Chance, Signals);
    }
}
