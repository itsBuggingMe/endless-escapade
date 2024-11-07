using System;
using Newtonsoft.Json;

namespace EndlessEscapade.Generators.Data;

public sealed class FootstepData : IEquatable<FootstepData>
{
    [JsonRequired]
    public SoundStyleData SoundStyleData;

    [JsonRequired]
    public string Material;

    public bool Equals(FootstepData other) {
        return other.SoundStyleData == SoundStyleData
            && other.Material == Material;
    }

    public override bool Equals(object obj) {
        return obj is FootstepData data && Equals(data);
    }

    public override int GetHashCode() {
        return HashCode.Combine(SoundStyleData, Material);
    }
}
