using EndlessEscapade.Framework.IO;
using ReLogic.Content.Sources;

namespace EndlessEscapade;

public sealed partial class EndlessEscapade : Mod
{
    public override IContentSource CreateDefaultContentSource()
    {
        var source = new RedirectContentSource(base.CreateDefaultContentSource());

        source.AddRedirect("Content", "Assets/Textures");

        return source;
    }
}