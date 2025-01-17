using System.Collections.Generic;

namespace EndlessEscapade.Core.Graphics;

[Autoload(Side = ModSide.Client)]
public abstract class RenderHook : ILoadable
{
    public abstract void Load(Mod mod);

    public abstract void Unload();
}