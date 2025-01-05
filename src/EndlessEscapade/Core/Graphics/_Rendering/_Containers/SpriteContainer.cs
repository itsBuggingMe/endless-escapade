using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

public struct SpriteContainer
{
    public List<Sprite> Entries
    {
        readonly get => _entries;
        private set
        {
            _entries = value;
            
            _entries.Sort(static (left, right) =>
            {
                var layerComparison = left.Layer.CompareTo(right.Layer);

                if (layerComparison != 0)
                {
                    return layerComparison;
                }

                return left.Parameters.Effect.Name.CompareTo(right.Parameters.Effect.Name);
            });
        }
    }

    private List<Sprite> _entries = [];
    
    public SpriteContainer() { }
}