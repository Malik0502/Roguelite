using Microsoft.Xna.Framework.Graphics;

namespace Engine.Core.Manager.ContentSystem;

public class SpriteRenderer
{
    private readonly IContentService _content;

    public SpriteRenderer(IContentService content)
    {
        _content = content;
    }

    public Texture2D GetTexture(string asset)
    {
        return _content.Load<Texture2D>(asset);
    }
}