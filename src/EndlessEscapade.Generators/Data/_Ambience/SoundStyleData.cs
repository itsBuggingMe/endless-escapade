using System;
using Newtonsoft.Json;

namespace EndlessEscapade.Generators.Data;

public sealed class SoundStyleData : IEquatable<SoundStyleData>
{
    [JsonRequired]
    public string SoundPath;

    public int Variants;

    public float Volume = 1f;

    public bool Equals(SoundStyleData other) {
        return other.SoundPath == SoundPath && other.Variants == Variants;
    }

    public override bool Equals(object obj) {
        return obj is SoundStyleData data && Equals(data);
    }

    public override int GetHashCode() {
        return HashCode.Combine(SoundPath, Variants);
    }
}
