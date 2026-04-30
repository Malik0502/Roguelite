using Engine.Core.Components;
using Engine.Core.Components.Collision;
using Engine.Core.Extensions;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.System;
using Game.Core.Systems.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems.Collision;

public class RectangleColliderRenderSystem : IDrawableSystem
{
    private readonly ComponentManager _componentManager;
    private ComponentPool<RectangleCollider> _rectangleColliderPool;
    private ComponentPool<Transform> _transformPool;

    private const bool ShouldRender = true;

    public RectangleColliderRenderSystem(ComponentManager componentManager)
    {
        _componentManager = componentManager;
    }
    
    public void Initialize()
    {
        _rectangleColliderPool = _componentManager.GetPool<RectangleCollider>();
        _transformPool = _componentManager.GetPool<Transform>();
    }

    public void Update(GameTime gameTime)
    {
        
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!ShouldRender) return;
        
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: RenderSystem.SpriteScaleMatrix);
        const int borderThickness = 2;
        
        foreach (var entity in _rectangleColliderPool.GetIds())
        {
            ref var collider =  ref _rectangleColliderPool.Get(entity);
            var entityPos = _transformPool.Get(entity).Position;

            collider.Rectangle.Location = entityPos.ToPoint();
            
            var colliderColor = collider.IsColliding ? Color.LimeGreen : Color.Red;
            
            spriteBatch.DrawRectangleOutline(collider.Rectangle, borderThickness, colliderColor);
        }
        spriteBatch.End();
    }
}