using System;
using Newtonsoft.Json;

namespace EndlessEscapade.Generators.Data;

public sealed class AmbienceSoundData : IEquatable<AmbienceSoundData>
{
    [JsonRequired]
    public int Chance;

    [JsonRequired]
    public string[] Signals;

    [JsonRequired]
    public string SoundPath;

    public int Variants = 1;

    public bool Equals(AmbienceSoundData other) {
        return other.SoundPath == SoundPath
            && other.Chance == Chance
            && other.Signals.AsSpan().SequenceEqual(Signals)
            && other.Variants == Variants;
    }

    public override bool Equals(object obj) {
        return Equals(obj as AmbienceSoundData);
    }

    public override int GetHashCode() {
        return HashCode.Combine(Signals.GetHashCode(), SoundPath, Chance, Variants);
    }
}
