using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

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

    public TiledMap GetTiledMap(string asset)
    {
        return _content.Load<TiledMap>(asset);
    }
}