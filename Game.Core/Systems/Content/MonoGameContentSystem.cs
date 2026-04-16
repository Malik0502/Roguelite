using Engine.Core.Manager.ContentSystem;
using Microsoft.Xna.Framework.Content;

namespace Game.Core.Systems.Content;

public class MonoGameContentSystem : IContentService
{
    private readonly ContentManager _contentManager;

    public MonoGameContentSystem(ContentManager contentManager)
    {
        _contentManager = contentManager;
    }

    public T Load<T>(string assetName)
    {
        return _contentManager.Load<T>(assetName);
    }
}