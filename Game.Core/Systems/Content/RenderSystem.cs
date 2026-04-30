using Engine.Core;
using Engine.Core.Components;
using Engine.Core.Extensions;
using Engine.Core.Manager.ComponentSystem;
using Engine.Core.Manager.SceneSystem;
using Engine.Core.Manager.System;
using Game.Core.Systems.Content.Tilemap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Core.Systems.Content;

public class RenderSystem : IDrawableSystem
{
    private readonly ComponentManager _componentManager;
    private readonly SceneManager _sceneManager;
    private readonly GraphicsDevice _graphics;
    private readonly TilemapRenderSystem _renderSystem;
    private ComponentPool<Sprite> _spritePool;
    private ComponentPool<Transform> _transformPool;

    private bool _shouldUpdate;
    public static Matrix SpriteScaleMatrix;

    public RenderSystem(ComponentManager componentManager, SceneManager sceneManager, GraphicsDevice graphics, TilemapRenderSystem renderSystem)
    {
        _componentManager = componentManager;
        _sceneManager = sceneManager;
        _graphics = graphics;
        _renderSystem = renderSystem;
    }

    public void Initialize()
    {
        _spritePool = _componentManager.GetPool<Sprite>();
        _transformPool = _componentManager.GetPool<Transform>();
        _shouldUpdate = true;
    }

    public void Update(GameTime gameTime)
    {
        if (!_shouldUpdate) return;
        UpdateScaleMatrix();
        _shouldUpdate = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _renderSystem.CameraMatrix);
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
        var screenScale = _graphics.Viewport.Width / 800f;
        
        // Create the scale transform for Draw. 
        // Do not scale the sprite depth (Z=1).
        SpriteScaleMatrix = Matrix.CreateScale(screenScale, screenScale, 1);
    }
}