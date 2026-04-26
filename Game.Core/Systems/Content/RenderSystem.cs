using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SceneSystem;
using Engine.Core.Manager.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems.Content;

public class RenderSystem : IDrawableSystem
{
    private readonly ComponentManager _componentManager;
    private readonly SceneManager _sceneManager;
    private readonly GraphicsDevice _graphics;
    private ComponentPool<Sprite> _spritePool;
    private ComponentPool<Transform> _transformPool;

    private bool _shouldUpdate;
    private Matrix _spriteScaleMatrix;

    public RenderSystem(ComponentManager componentManager, SceneManager sceneManager, GraphicsDevice graphics)
    {
        _componentManager = componentManager;
        _sceneManager = sceneManager;
        _graphics = graphics;
    }

    public void Initialize()
    {
        _spritePool = _componentManager.GetPool<Sprite>();
        _transformPool = _componentManager.GetPool<Transform>();
        _shouldUpdate = true;
    }

    public void Update(float deltaTime)
    {
        if (!_shouldUpdate) return;
        UpdateScaleMatrix();
        _shouldUpdate = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _spriteScaleMatrix);
        foreach (var entity in _sceneManager.GetCurrentScene().GetEntities())
        {
            DrawEntity(entity, spriteBatch);
        }
        spriteBatch.End();
    }

    private void DrawEntity(Entity entity, SpriteBatch spriteBatch)
    {
        var entityTexture = _spritePool.Get(entity.Id).Texture;
        var entityTransform = _transformPool.Get(entity.Id);

        spriteBatch.Draw(entityTexture, entityTransform.Position, entityTransform.Scale);
    }

    private void UpdateScaleMatrix()
    {
        // Default resolution is 800x600; scale sprites up or down based on
        // current viewport
        float screenScale = _graphics.Viewport.Width / 800f;
        
        // Create the scale transform for Draw. 
        // Do not scale the sprite depth (Z=1).
        _spriteScaleMatrix = Matrix.CreateScale(screenScale, screenScale, 1);
    }
}