using System.Linq;
using Engine.Core.Components.Collision;
using Engine.Core.Components.Tags;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems.Collision;

public class RectangleColliderRenderSystem : IDrawableSystem
{
    private readonly ComponentManager _componentManager;
    private ComponentPool<RectangleCollider> _rectangleColliderPool;
    private int _playerId;

    public RectangleColliderRenderSystem(ComponentManager componentManager)
    {
        _componentManager = componentManager;
    }
    
    public void Initialize()
    {
        _playerId = _componentManager.GetPool<PlayerTag>().GetIds().First();
        _rectangleColliderPool = _componentManager.GetPool<RectangleCollider>();
    }

    public void Update(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        throw new System.NotImplementedException();
    }
}